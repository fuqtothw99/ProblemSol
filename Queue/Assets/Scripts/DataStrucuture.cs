using System;

namespace DataStructure
{
    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        public LinkedListNode<T> Head { get; set; } // 접근자를 public으로 변경

        public LinkedList()
        {
            Head = null;
        }

        public void Add(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                LinkedListNode<T> current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public T RemoveLast()
        {
            if (Head == null)
            {
                throw new InvalidOperationException("LinkedList is empty.");
            }

            if (Head.Next == null)
            {
                T data = Head.Data;
                Head = null;
                return data;
            }

            LinkedListNode<T> current = Head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }

            T removedData = current.Next.Data;
            current.Next = null;
            return removedData;
        }
    }

    public class Queue<T>
    {
        private LinkedList<T> list;

        public Queue()
        {
            list = new LinkedList<T>();
        }

        public void Enqueue(T data)
        {
            list.Add(data);
        }

        public T Dequeue()
        {
            if (list.Head == null)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T data = list.Head.Data;
            list.Head = list.Head.Next;
            return data;
        }
    }

    public class Stack<T>
    {
        private LinkedList<T> list;

        public Stack()
        {
            list = new LinkedList<T>();
        }

        public void Push(T data)
        {
            list.Add(data);
        }

        public T Pop()
        {
            if (list.Head == null)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            return list.RemoveLast();
        }
    }
}
