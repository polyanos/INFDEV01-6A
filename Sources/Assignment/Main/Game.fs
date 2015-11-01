module VirtualCity
  (*
  TODO:

  House2 e House3 non sono ancora trovate nel tileset
  edifici civici (ospedali/polizia)
  strade a griglia (rimozioni ma non da MST con probabilità variabile rispetto a densità abitativa)
  traffico e macchine
  algoritmi dell'utente e gestione dell'input
  *)


  open Microsoft.Xna.Framework
  open Microsoft.Xna.Framework.Graphics
  open Microsoft.Xna.Framework.Input
  open Microsoft.Xna.Framework.Content

  type Microsoft.Xna.Framework.Game with member this.Self = this
  type Microsoft.Xna.Framework.GameTime with member this.DT = this.ElapsedGameTime.TotalSeconds |> float32
  type Tile = Tile.Description

  type Simulation() =
    inherit Game()

    member val graphics = new GraphicsDeviceManager(base.Self) with get
    [<DefaultValue>]
    val mutable spriteBatch : SpriteBatch
    [<DefaultValue>]
    val mutable tileSet : Texture2D

    let random = System.Random()
    let nature_density = Perlin.noise 256 4 (Some 7)
    let abitation_density = Perlin.noise 256 4 (Some 13)
    let mutable planes = [] : List<Vector2>
    let map_width,map_height = 96,48
    let trees =
      let tree_random = System.Random()
      [| for i = 0 to map_width do yield [| for j = 0 to map_height do yield tree_random.NextDouble() > 0.5 |]|]

    let mutable camera_x, camera_y, zoom = 0.0f,0.0f,1.0f
    let clamp (a:float32) b c = MathHelper.Clamp(a,b,c)
      
    override this.Initialize() =
      base.Content.RootDirectory <- "Content"
      this.spriteBatch <- new SpriteBatch(this.GraphicsDevice)
      this.tileSet <- this.Content.Load "tileset.png"

    override this.Update gt =
      let ks = Keyboard.GetState()
      let camera_speed = 300.0f
      if ks.[Keys.Left] = KeyState.Down then
        camera_x <- camera_x + camera_speed * gt.DT * zoom
      if ks.[Keys.Right] = KeyState.Down then
        camera_x <- camera_x - camera_speed * gt.DT * zoom
      if ks.[Keys.Up] = KeyState.Down then
        camera_y <- camera_y + camera_speed * gt.DT * zoom
      if ks.[Keys.Down] = KeyState.Down then
        camera_y <- camera_y - camera_speed * gt.DT * zoom
      if ks.[Keys.Z] = KeyState.Down then
        zoom <- zoom + 10.0f * gt.DT
      if ks.[Keys.X] = KeyState.Down then
        zoom <- zoom - 10.0f * gt.DT
      zoom <- clamp zoom 1.0f 3.0f
//      camera_x <- clamp camera_x (-(float32 map_width * 16.0f - float32 base.GraphicsDevice.Viewport.Width) / zoom) 0.0f
//      camera_y <- clamp camera_y (-(float32 map_height * 16.0f - float32 base.GraphicsDevice.Viewport.Height) / zoom) 0.0f
      base.Update(gt)

    override this.Draw gt =
      do this.GraphicsDevice.Clear(Color.CornflowerBlue)

      let sb = this.spriteBatch
      let tileset = this.tileSet
      do sb.Begin(transformMatrix = System.Nullable(Matrix.CreateScale(zoom) * Matrix.CreateTranslation(camera_x, camera_y, 0.0f)))
      for i = 0 to map_width do
        for j = 0 to map_height do
          let tile = 
            if nature_density.[i].[j] < 40.0f then Tile.Water
            elif nature_density.[i].[j] < 45.0f then Tile.Sand
            elif nature_density.[i].[j] > 70.0f then Tile.Grass
            else Tile.Cement
          do Tile.draw(tileset,sb,(i * 16, j * 16, 16, 16),tile)
          if tile = Tile.Grass && trees.[i].[j] then 
            do Tile.draw(tileset,sb,(i * 16, j * 16, 16, 16),Tile.Tree)
          if tile = Tile.Cement then
            if abitation_density.[i].[j] < 70.0f then
              if (i * i - j) % 3 = 0 then
                do Tile.draw(tileset,sb,(i * 16 + 4, j * 16 + 4, 10, 10),Tile.House1)
              elif (i * i - j) % 3 = 1 then
                do Tile.draw(tileset,sb,(i * 16 + 4, j * 16 + 4, 10, 10),Tile.House2)
              else
                do Tile.draw(tileset,sb,(i * 16 + 4, j * 16 + 4, 10, 10),Tile.House3)
            else
              if (i * i - j) % 3 = 0 then
                do Tile.draw(tileset,sb,(i * 16, j * 16, 16, 16),Tile.SmallCondo1)
              elif (i * i - j) % 3 = 1 then
                do Tile.draw(tileset,sb,(i * 16, j * 16, 16, 16),Tile.SmallCondo2)
              else
                do Tile.draw(tileset,sb,(i * 16, j * 16 + 4, 16, 10),Tile.SmallCondo3)
      planes <-
        [
          for p in planes do
            do Tile.draw(tileset,sb,p,Tile.Plane)
            if p.Y < 600.0f then
              yield p + Vector2.UnitY * gt.DT * 20.0f
          if random.NextDouble() > 0.9999 then
            yield Vector2(float32(random.NextDouble()) * (float32 map_width * 16.0f), -80.0f)
        ]

      do sb.End()

      base.Draw(gt)
