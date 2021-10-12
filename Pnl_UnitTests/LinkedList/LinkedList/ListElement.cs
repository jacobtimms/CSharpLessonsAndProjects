using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    public class ListElement<T>
    {
        public ListElement<T> next;
        public T value;
        public ListElement(T newVal)
        {
            value = newVal;
        }
        public ListElement(){}
    }
}
