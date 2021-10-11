using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedLists_3
{

    public class Node<T>
    {
        public T data;
        public Node<T> next;

        public Node(T i)
        {
            data = i;
            next = null;
        }

        public void Print()
        {
            Console.Write("|" + data + "|");
            if (next != null)
            {
                next.Print();
            }
        }
    }


    public class MyList<T> : ILinkedList<T>
    {
        public Node<T> headNode;

        public MyList()
        {
            headNode = null;
        }

        /// ADD AT START
        public void Add(T data)
        {
            if (headNode == null)
            {
                headNode = new Node<T>(data);
            }
            else
            {
                //Insert node before headNode
                Node<T> temp = new Node<T>(data);
                temp.next = headNode;
                headNode = temp;
            }
        }

        /// ADD AT POSITION
        public void AddAtPosition(int position, T data)
        {
            if (headNode == null && position != 0)
            {
                throw new ArgumentException("Position does not exist");
            }
            else if (position == 0)
            {
                this.Add(data);
            }
            else
            {
                //Create new node & insert at position
                Node<T>[] nodePositions = GetNodePositions(position);

                Node<T> nodeInsert = new Node<T>(data);
                nodeInsert.next = nodePositions[0];
                nodePositions[1].next = nodeInsert;
            }

        }

        /// ADD AFTER DATA
        public void AddAfterData(T dataToAddAfter, T nextData)
        {
            //Loop through the linked list until data match has been found
            Node<T> currentNode = this.headNode;
            Node<T> previousNode = null;

            while (currentNode.data.Equals(dataToAddAfter))
            {
                previousNode = currentNode;
                currentNode = currentNode.next;

                if (currentNode == null)
                {
                    throw new ArgumentException("Position does not exist");
                }
            }
                //Position on the next node, to ensure new node is paced in front
                previousNode = currentNode;
                currentNode = currentNode.next;
                //Create new node & insert at position
                Node<T> nodeInsert = new Node<T>(nextData);
                nodeInsert.next = currentNode;
                previousNode.next = nodeInsert;
        }

        //DELETE AT POSITION
        public void Delete(int position)
        {
            if (position == 0)
            {
                headNode = headNode.next;
                return;
            }
            Node<T>[] nodePositions = GetNodePositions(position);
            nodePositions[1].next = nodePositions[0].next;
        }

        //DELETE SPECIFIC DATA
        public void Delete(T data)
        {
            Node<T> currentNode = this.headNode;
            Node<T> previousNode = null;

            //Loop through the linked list & delete node's with matching data,
            while (currentNode != null)
            {

                if (headNode.data.Equals(data))
                {
                    headNode = headNode.next;
                    return;
                }
                else if(data.Equals(currentNode.data))
                {
                    previousNode.next = currentNode.next;
                    return;
                }
                previousNode = currentNode;
                currentNode = currentNode.next;
            }
            throw new ArgumentException("Data cannot be found");
        }

        //GET POSITION DATA
        public T Get(int position)
        {
            Node<T>[] nodePositions = GetNodePositions(position);
            return nodePositions[0].data;
        }

        //REPLACE DATA AT POSITION
        public void Replace(int position, T newData)
        {
            Node<T>[] nodePositions = GetNodePositions(position);
            nodePositions[0].data = newData;
        }

        //GET NODE POSITION
        public Node<T>[] GetNodePositions(int position)
        {
            //Loop through the linked list until position has been reached
            Node<T> currentNode = this.headNode;
            Node<T> previousNode = null;

            int i = 0;
            while (i < position)
            {
                previousNode = currentNode;
                currentNode = currentNode.next;

                if (currentNode == null)
                {
                    throw new ArgumentException("Position does not exist");
                }
                i++;
            }
            //Return current & previous Node
            Node<T>[] returnValues = new Node<T>[] { currentNode, previousNode };
            return returnValues;
        }

        public void Print()
        {
            if (headNode != null)
            {
                headNode.Print();
            }
        }
    }
}
