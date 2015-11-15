using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace EntryPoint
{
#if WINDOWS || LINUX
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, or 3)", "Choose assignment", "1"))
      {
        case "1":
          using (var game = VirtualCity.RunAssignment1(Assignment1))
            game.Run();
          break;
        case "2":
          using (var game = VirtualCity.RunAssignment2(Assignment2))
            game.Run();
          break;
        case "3":
          using (var game = VirtualCity.RunAssignment3(Assignment3))
            game.Run();
          break;
        default:
          break;
      }
    }

    private static IEnumerable<Vector2> Assignment1(Vector2 arg1, IEnumerable<Vector2> arg2)
    {
      throw new NotImplementedException();
    }

    private static IEnumerable<IEnumerable<Vector2>> Assignment2(IEnumerable<Vector2> arg1, IEnumerable<Tuple<Vector2, double>> arg2)
    {
      throw new NotImplementedException();
    }

    private static IEnumerable<Tuple<Vector2, Vector2>> Assignment3(Vector2 arg1, Vector2 arg2, IEnumerable<Tuple<Vector2, Vector2>> arg3)
    {
      throw new NotImplementedException();
    }
  }
#endif
}
