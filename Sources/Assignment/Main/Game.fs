module VirtualCity
  (*
  TODO:

  edifici civici (ospedali/polizia)
  algoritmi dell'utente e gestione dell'input
    1. data una casa scelta da noi, ordinare per distanza ogni altra special building
       - Vector2 * IEnumerable<Vector2> -> IEnumerable<Vector2>
       - evidenziare graficamente l'oggetto selezionato e quelli da sortare
       - ricevere la lista sortata e mostra l'indice per distanza in sovraimpressione sull'edificio
    2. data una casa e un ospedale, trova lo shortest path tra i due
       - Vector2 * Vector2 * IEnumerable<Vector2 * Vector2> -> IEnumerable<Vector2>
       - evidenziare graficamente i due edifici
       - evidenziare graficamente il percorso
    3. data una casa, trova lo shortest path tra essa e tutti gli special buildings
       - Vector2 * IEnumerable<Vector2> * IEnumerable<Vector2 * Vector2> -> IEnumerable<IEnumerable<Vector2>>
  three types of tiles instead of two (split GroundAndBuilding into Ground and Building)
  traffico e macchine

  *)


  open Microsoft.Xna.Framework
  open Microsoft.Xna.Framework.Graphics
  open Microsoft.Xna.Framework.Input
  open Microsoft.Xna.Framework.Content

  type Microsoft.Xna.Framework.Game with member this.Self = this
  type Microsoft.Xna.Framework.GameTime with member this.DT = this.ElapsedGameTime.TotalSeconds |> float32
  type Ground = Tile.Ground
  type Building = Tile.Building
  type Transportation = Tile.Transportation
  type Road = Tile.Road

  type Simulation() =
    inherit Game()

    member val graphics = new GraphicsDeviceManager(base.Self) with get
    [<DefaultValue>]
    val mutable spriteBatch : SpriteBatch
    [<DefaultValue>]
    val mutable tileSet : Texture2D
    [<DefaultValue>]
    val mutable roadTileSet : Texture2D

    let map_width,map_height = 96,48
    let random = System.Random()
    let mutable planes = [] : List<Vector2>

    let mutable camera_x, camera_y, zoom = 0.0f,0.0f,10.0f
    let clamp (a:float32) b c = MathHelper.Clamp(a,b,c)

    let map = MapGenerator.generate_map map_width map_height
    let full_crossings =
      [| for i = 0 to map_width do
           yield [|
             for j = 0 to map_height do
               yield
                  map.Roads |> Set.contains ((i,j),(i+1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i-1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j+1)) && 
                  map.Roads |> Set.contains ((i,j),(i,j-1)) |] |]
    let three_crossings =
      [| for i = 0 to map_width do
           yield [|
             for j = 0 to map_height do
               if map.Roads |> Set.contains ((i,j),(i-1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i+1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j+1)) then
                 yield Some Road.Three3
               elif map.Roads |> Set.contains ((i,j),(i-1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i+1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j-1)) then
                 yield Some Road.Three2
               elif map.Roads |> Set.contains ((i,j),(i-1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j+1)) && 
                  map.Roads |> Set.contains ((i,j),(i,j-1)) then
                 yield Some Road.Three4
               elif map.Roads |> Set.contains ((i,j),(i+1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j+1)) && 
                  map.Roads |> Set.contains ((i,j),(i,j-1)) then
                 yield Some Road.Three1
               else
                 yield None |] |]
    let curves =
      [| for i = 0 to map_width do
           yield [|
             for j = 0 to map_height do
               if map.Roads |> Set.contains ((i,j),(i+1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j+1)) then
                 yield Some Road.Curve3
               elif map.Roads |> Set.contains ((i,j),(i-1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j+1)) then
                 yield Some Road.Curve4
               elif map.Roads |> Set.contains ((i,j),(i-1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j-1)) then
                 yield Some Road.Curve2
               elif map.Roads |> Set.contains ((i,j),(i+1,j)) && 
                  map.Roads |> Set.contains ((i,j),(i,j-1)) then
                 yield Some Road.Curve1
               else
                 yield None |] |]

    let short_connectors =
      [| for i = 0 to map_width do
           yield [|
             for j = 0 to map_height do
               if map.Roads |> Set.contains ((i,j),(i+1,j)) then
                 yield Some Road.Two3
               elif map.Roads |> Set.contains ((i,j),(i,j+1)) then
                 yield Some Road.Two4
               else
                 yield None |] |]
      
    override this.Initialize() =
      base.Content.RootDirectory <- "Content"
      this.spriteBatch <- new SpriteBatch(this.GraphicsDevice)
      this.tileSet <- this.Content.Load "tileset.png"
      this.roadTileSet <- this.Content.Load "roads.jpg"

    override this.Update gt =
      let ks = Keyboard.GetState()
      let camera_speed = 300.0f
      if ks.[Keys.Escape] = KeyState.Down then
        do this.Exit()
      if ks.[Keys.A] = KeyState.Down then
        camera_x <- camera_x - camera_speed * gt.DT * zoom
      if ks.[Keys.D] = KeyState.Down then
        camera_x <- camera_x + camera_speed * gt.DT * zoom
      if ks.[Keys.W] = KeyState.Down then
        camera_y <- camera_y - camera_speed * gt.DT * zoom
      if ks.[Keys.S] = KeyState.Down then
        camera_y <- camera_y + camera_speed * gt.DT * zoom
      if ks.[Keys.Z] = KeyState.Down then
        zoom <- zoom + 10.0f * gt.DT
      if ks.[Keys.X] = KeyState.Down then
        zoom <- zoom - 10.0f * gt.DT
      zoom <- clamp zoom 4.0f 4.0f
      let screen_width,screen_height = float32 this.GraphicsDevice.Viewport.Width, float32 this.GraphicsDevice.Viewport.Height
      camera_x <- clamp camera_x 0.0f (float32 map_width * 16.0f - screen_width / 4.0f + 16.0f)
      camera_y <- clamp camera_y 0.0f (float32 map_height * 16.0f - screen_height / 4.0f + 16.0f)
      base.Update(gt)

    override this.Draw gt =
      do this.GraphicsDevice.Clear(Color.CornflowerBlue)

      let sb = this.spriteBatch
      let tileset = this.tileSet
      let roadTileSet = this.roadTileSet
      do sb.Begin(transformMatrix = System.Nullable(Matrix.CreateTranslation(-camera_x, -camera_y, 0.0f) * Matrix.CreateScale(zoom)))
      for i = 0 to map_width do
        for j = 0 to map_height do
          do Ground.draw(tileset,sb,(i * 16, j * 16, 16, 16),map.Ground.[i].[j])
          if map.Trees.[i].[j] then 
            do Ground.draw(tileset,sb,(i * 16, j * 16, 16, 16),Tile.Tree)
          match map.Buildings.[i].[j] with
          | Some tile -> 
              do Building.draw(tileset,sb,(i * 16 + 4, j * 16 + 4, 10, 10),tile)
          | None -> ()
      for i = 0 to map_width do
        for j = 0 to map_height do
          if full_crossings.[i].[j] then
            do Road.draw(roadTileSet,sb,(i * 16 - 2, j * 16 - 2, 4, 4),Road.Four)
          else
            match three_crossings.[i].[j] with
            | Some tile ->
              do Road.draw(roadTileSet,sb,(i * 16 - 2, j * 16 - 2, 4, 4),tile)
            | None -> 
              match curves.[i].[j] with
              | Some tile ->
                do Road.draw(roadTileSet,sb,(i * 16 - 2, j * 16 - 2, 4, 4),tile)
              | None ->
                match short_connectors.[i].[j] with
                | Some tile ->
                  do Road.draw(roadTileSet,sb,(i * 16 - 2, j * 16 - 2, 4, 4),tile)
                | None -> ()
      for ((i,j),(i',j')) in map.Roads do
          if i' = i + 1 then
            do Road.draw(roadTileSet,sb,(i * 16 + 2, j * 16 - 2, 12, 4),Road.Two1)
          elif j' = j + 1 then
            do Road.draw(roadTileSet,sb,(i * 16 - 2, j * 16 + 2, 4, 12),Road.Two2)
      planes <-
        [
          for p in planes do
            do Transportation.draw(tileset,sb,p,Tile.Plane)
            if p.Y < 600.0f then
              yield p + Vector2.UnitY * gt.DT * 40.0f
          if random.NextDouble() > 0.9999 then
            yield Vector2(float32(random.NextDouble()) * (float32 map_width * 16.0f), -80.0f)
        ]

      do sb.End()

      base.Draw(gt)
