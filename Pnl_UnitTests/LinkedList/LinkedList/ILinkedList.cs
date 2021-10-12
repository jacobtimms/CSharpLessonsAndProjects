using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    public interface ILinkedList<T>: IEnumerable
    {
        /// <summary>
        /// add at position
        /// </summary>
        void AddAtPos(int postion, T data);
        /// <summary>
        /// add after data
        /// </summary>
        void AddAfterData(T dataToAddAfter, T nextData);
        /// <summary>
        /// add at start
        /// </summary>
        void AddAtStart(T data);


        void DeletePos(int postion);
        void DeleteData(T data);

        ListElement<T> Get(int position);

        void Replace(T oldData, T newData);
    }
}
