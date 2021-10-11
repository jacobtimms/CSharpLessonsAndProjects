using System;
namespace LinkedLists_1
{
    public interface ILinkedList
    {
        /// <summary>
        /// add at position
        /// </summary>
        void Add(int postion, string data);
        /// <summary>
        /// add after data
        /// </summary>
        void Add(string dataToAddAfter, string nextData);
        /// <summary>
        /// add at start
        /// </summary>
        void Add(string data);


        void Delete(int postion);
        void Delete(string data);

        string Get(int position);

        string Replace(string oldData, string newData);
    }
}
