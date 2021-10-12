using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    class LList<T> : ILinkedList<T>
    {
        public ListElement<T> _firstElement;
        public IEnumerator<T> GetEnumerator()
        {
            var current = _firstElement;
            while (current != null)
            {
                yield return current.value;
                current = current.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddAtPos(int position, T data)
        {
            // at position
            var newElement = new ListElement<T>(data);
            var currentElement = _firstElement;
            if (position == 0)
            {
                newElement.next = currentElement;
                _firstElement = newElement;
            }
            else
            {
                for (int x = 1; x < position; x++)
                {
                    if (currentElement.next == null)
                    {
                        throw new Exception("Out of bounds");
                    }
                    currentElement = currentElement.next;
                }
                newElement.next = currentElement.next;
                currentElement.next = newElement;
            }
        }
        public void AddAfterData(T dataToAddAfter, T nextData)
        {
            // add after found data
            var currentElement = _firstElement;
            while (!currentElement.value.Equals(dataToAddAfter))
            {
                if (currentElement.next == null)
                {
                    throw new Exception("Out of bounds");
                }
                currentElement = currentElement.next;
            }
            var newElement = new ListElement<T>(nextData);
            newElement.next = currentElement.next;
            currentElement.next = newElement;
        }
        public void AddAtStart(T data)
        {
            // add at start
            if (_firstElement == null)
            {
                _firstElement = new ListElement<T>(data);
            }
            else
            {
                var newElement = new ListElement<T>(data);
                newElement.next = _firstElement;
                _firstElement = newElement;
            }
        }
        public void DeletePos(int position)
        {
            var elementToDelete = _firstElement;
            var previousElement = _firstElement;
            for (int x = 0; x < position; x++)
            {
                previousElement = elementToDelete;
                elementToDelete = elementToDelete.next;
            }
            if (elementToDelete == _firstElement)
            {
                _firstElement = _firstElement.next;
            }
            else
            {
                previousElement.next = elementToDelete.next;
            }
        }
        public void DeleteData(T data)
        {
            var elementToDelete = _firstElement;
            var previousElement = _firstElement;
            while (!elementToDelete.value.Equals(data))
            {
                previousElement = elementToDelete;
                elementToDelete = elementToDelete.next;
            }
            if (elementToDelete.value.Equals(data))
            {
                if (elementToDelete == _firstElement)
                {
                    _firstElement = _firstElement.next;
                }
                else
                {
                    previousElement.next = elementToDelete.next;
                }
            }
        }
        public void Replace(T oldData, T newData)
        {
            var newElement = new ListElement<T>(newData);
            var elementToReplace = _firstElement;
            var previousElement = _firstElement;
            if (_firstElement.value.Equals(oldData))
            {
                newElement.next = _firstElement.next;
                _firstElement = newElement;
            }
            else
            {
                while (!elementToReplace.value.Equals(oldData))
                {
                    previousElement = elementToReplace;
                    elementToReplace = elementToReplace.next;
                }

                if (previousElement != null)
                {
                    previousElement.next = newElement;
                }
                newElement.next = elementToReplace.next;
                elementToReplace.next = newElement;
            }
        }
        public ListElement<T> Get(int position)
        {
            var returnElement = _firstElement;
            for (int x = 0; x < position; x++)
            {
                returnElement = returnElement.next;
            }
            return returnElement;
        }
        public void PrintAll()
        {
            var currentEl = _firstElement;
            while (currentEl != null)
            {
                Console.WriteLine(currentEl.value);
                currentEl = currentEl.next;
            }
        }
    }
}
