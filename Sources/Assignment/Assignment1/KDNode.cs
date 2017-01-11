namespace Assignment1
{
    public interface KDNode<T>
    {
        KDNode<T> Parent { get; }
        KDNode<T> LeftChild { get; set; }
        KDNode<T> RightChild { get; set; }
        Dimension Dimension { get; }
        T Value { get; }

        bool HasLeftChild();
        bool HasRightChild();
    }
}