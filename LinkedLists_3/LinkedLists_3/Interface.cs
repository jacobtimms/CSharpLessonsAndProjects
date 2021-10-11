using System;
namespace LinkedLists_3
{
    public interface ILinkedList<T>
    {
        /// <summary>
        /// add at position
        /// </summary>
        void AddAtPosition(int postion, T data);
        /// <summary>
        /// add after data
        /// </summary>
        void AddAfterData(T dataToAddAfter, T nextData);
        /// <summary>
        /// add at start
        /// </summary>
        void Add(T data);


        void Delete(int postion);
        void Delete(T data);

        T Get(int position);

        void Replace(int position, T newData);
    }
}
