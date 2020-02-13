namespace uk.co.ytfox.MCWrap
{
    class Rotator<T>
    {
        T[] content;
        int position;
        public readonly int Length;
        public T this[int index] {
            get
            {
                return content[(position + index) % content.Length];
            }
        }
        public void append(T input)
        {
            content[position % content.Length] = input;
            position++;
        }
        public Rotator(int length){
            content = new T[length];
            position = 0;
            Length = length;
        }

    }
}
