using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNullEmpty
{
  interface ITree<T>
  {
    bool IsEmpty { get; }
    T Value { get; }
    ITree<T> Left { get; }
    ITree<T> Right { get; }
  }

  class Empty<T> : ITree<T>
  {
    public bool IsEmpty
    {
      get
      {
        return true;
      }
    }

    public ITree<T> Left
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public ITree<T> Right
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public T Value
    {
      get
      {
        throw new NotImplementedException();
      }
    }
  }

  class Node<T> : ITree<T>
  {
    public bool IsEmpty
    {
      get
      {
        return false;
      }
    }

    public Node(ITree<T> l, T v, ITree<T> r) { Left = l; Value = v; Right = r; }

    public ITree<T> Left { get; set; }
    public ITree<T> Right { get; set; }
    public T Value { get; set; }
  }

  class Program
  {
    static void Main(string[] args)
    {
      var t = new Empty<int>() as ITree<int>;
      t = Insert(t, 50);
      t = Insert(t, 25);
      t = Insert(t, 75);
      t = Insert(t, 100);
      t = Insert(t, 7);
      t = Insert(t, 35);
      PrintInOrder(t);
      Console.WriteLine(ContainsKey(t, 91));
    }

    static ITree<int> Insert(ITree<int> t, int v)
    {
      if (t.IsEmpty)
        return new Node<int>(new Empty<int>(), v, new Empty<int>());
      else
      {
        if (t.Value == v)
          return t;
        else
        {
          if (t.Value > v)
            return new Node<int>(Insert(t.Left, v), t.Value, t.Right);
          else
            return new Node<int>(t.Left, t.Value, Insert(t.Right, v));
        }
      }

    }

    static bool ContainsKey(ITree<int> r, int v)
    {
      if (r.IsEmpty)
        return false;
      else
      {
        Console.WriteLine("Comparing with..." + r.Value + "!!!");
        if (r.Value == v)
          return true;
        else
        {
          if (r.Value > v)
            return ContainsKey(r.Left, v);
          else
            return ContainsKey(r.Right, v);
        }
      }
    }

    static void PrintPreOrder(ITree<int> r)
    {
      if (r.IsEmpty)
        return;
      else
      {
        Console.WriteLine(r.Value);
        PrintPreOrder(r.Left);
        PrintPreOrder(r.Right);
      }
    }

    static void PrintPostOrder(ITree<int> r)
    {
      if (r.IsEmpty)
        return;
      else
      {
        PrintPostOrder(r.Left);
        PrintPostOrder(r.Right);
        Console.WriteLine(r.Value);
      }
    }

    static void PrintInOrder(ITree<int> r)
    {
      if (r.IsEmpty)
        return;
      else
      {
        PrintInOrder(r.Left);
        Console.WriteLine(r.Value);
        PrintInOrder(r.Right);
      }
    }
  }
}
