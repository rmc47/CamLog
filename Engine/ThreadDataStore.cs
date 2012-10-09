using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Engine
{
    internal sealed class ThreadDataStore<T>
    {
        private ReaderWriterLock m_InnerDictionaryLock = new ReaderWriterLock();
        private readonly Dictionary<int, T> m_InnerDictionary = new Dictionary<int, T>();

        public T Object
        {
            get
            {
                int currentThreadID = Thread.CurrentThread.ManagedThreadId;
                T threadObject;

                m_InnerDictionaryLock.AcquireReaderLock(1000); // Really never going to be >1s
                try
                {
                    if (!m_InnerDictionary.TryGetValue(currentThreadID, out threadObject))
                        return default(T);
                    else
                        return threadObject;
                }
                finally
                {
                    m_InnerDictionaryLock.ReleaseLock();
                }
            }
            set
            {
                int currentThreadID = Thread.CurrentThread.ManagedThreadId;
                m_InnerDictionaryLock.AcquireWriterLock(1000); // Really never going to be >1s
                try
                {
                    m_InnerDictionary[currentThreadID] = value;
                }
                finally
                {
                    m_InnerDictionaryLock.ReleaseLock();
                }
            }
        }
    }
}
