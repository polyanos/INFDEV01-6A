module Tile

  open Microsoft.Xna.Framework
  open Microsoft.Xna.Framework.Graphics
  open Microsoft.Xna.Framework.Input
  open Microsoft.Xna.Framework.Content

  type Microsoft.Xna.Framework.Game with member this.Self = this
  type Microsoft.Xna.Framework.GameTime with member this.DT = this.ElapsedGameTime.TotalSeconds |> float32

  type Description = 
    | Grass
    | Sand
    | Water
    | Tree
    | Cement
    | Plane
    | SmallCondo1
    | SmallCondo2
    | SmallCondo3
    | House1
    | House2
    | House3
    with 
      member tile.Rectangle =
        match tile with
        | Grass         -> Rectangle(215, 152, 240-215, 178-152)
        | Sand          -> Rectangle(116, 286, 134-116, 300-286)
        | Water         -> Rectangle(134, 179, 161-134, 205-179)
        | Tree          -> Rectangle(10, 0, (115-10)/4, 28-0)
        | Cement        -> Rectangle(313, 188, 339-313, 214-188)
        | Plane         -> Rectangle(451, 356, 538-451, 430-356)
        | SmallCondo1   -> Rectangle(508, 561, 577-508, 656-561)
        | SmallCondo2   -> Rectangle(526, 666, 601-526, 782-666)
        | SmallCondo3   -> Rectangle(601, 676, 708-601, 774-676)
        | House1        -> Rectangle(579, 596, 626-579, 651-596)
        | House2        -> Rectangle(579, 596, 626-579, 651-596) // da trovare
        | House3        -> Rectangle(579, 596, 626-579, 651-596) // da trovare
      static member draw((tileset:Texture2D),(sb:SpriteBatch),(px,py,sx,sy),(t:Description)) =
        sb.Draw(tileset, Rectangle(px,py,sx,sy), System.Nullable(t.Rectangle), Color.White)
      static member draw((tileset:Texture2D),(sb:SpriteBatch),(p:Vector2),(t:Description)) =
        sb.Draw(tileset, p, System.Nullable(t.Rectangle), Color.White)
