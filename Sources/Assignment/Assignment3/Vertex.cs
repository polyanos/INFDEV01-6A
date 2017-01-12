namespace Assignment3
{
    public interface Vertex<T, Y>
    {
        string Name { get; }
        T Value { get; }
        Y Distance { get; set; }
        VectorVertex Previous { get; set; }
    }
}