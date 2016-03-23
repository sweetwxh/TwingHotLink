using System;
using System.Collections.Generic;
using System.Net.Sockets;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink.Server.Extensions
{
    internal class SocketArgsPool : IDisposable
    {
        private readonly Stack<SocketAsyncEventArgs> argsPool;

        /// <summary>
        ///     SocketAsyncEventArgs的管理池，以避免重复分配。
        /// </summary>
        /// <param name="capacity"></param>
        public SocketArgsPool(int capacity)
        {
            argsPool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        /// <summary>
        ///     获取当前数据池中的SocketAsyncEventArgs实例的个数
        /// </summary>
        public int Count
        {
            get { return argsPool.Count; }
        }

        /// <summary>
        ///     将一个SocketAsyncEventArgs放入堆栈
        /// </summary>
        /// <param name="item"></param>
        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("添加的SocketAsyncEventArgsPool项不能为空");
            }
            lock (argsPool)
            {
                argsPool.Push(item);
            }
        }

        /// <summary>
        ///     将一个SocketAsyncEventArgs推出堆栈
        /// </summary>
        /// <returns></returns>
        public SocketAsyncEventArgs Pop()
        {
            lock (argsPool)
            {
                return argsPool.Pop();
            }
        }

        #region IDisposable 成员

        private bool disposed;

        ~SocketArgsPool()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    foreach (var args in argsPool)
                    {
                        args.Dispose();
                    }
                }
                disposed = true;
            }
        }

        #endregion
    }
}