using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public class Node<T>
    {
        public T data;
        public Node<T> next;

        public Node(T data)
        {
            this.data = data;
            this.next = null;
        }
    }

    public class Queue<T>
    {
        private Node<T> front;
        private Node<T> rear;
        private int count;

        public Queue()
        {
            front = null;
            rear = null;
            count = 0;
        }

        public void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (rear == null)
            {
                front = newNode;
                rear = newNode;
            }
            else
            {
                rear.next = newNode;
                rear = newNode;
            }

            count++;
        }

        public T Dequeue()
        {
        if (front == null)
        {
            Debug.LogError("Queue is empty"); 

            return default(T);
        }

            T data = front.data;
            front = front.next;

            if (front == null)
            {
                rear = null;
            }

            count--;
            return data;
        }

        public int Count()
        {
            return count;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }
    }
