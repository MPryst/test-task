using BackEnd.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BackEnd.Transactions.Services
{

	public class SynchronizedCache
	{
		private double balance;	

		private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

		private Dictionary<Guid, Transaction> innerCache = new Dictionary<Guid, Transaction>();

		public SynchronizedCache()
		{
			balance = 0d;
			innerCache = new Dictionary<Guid, Transaction>();
		}

		public Transaction Read(string key)
		{
			cacheLock.EnterReadLock();
			try
			{
				var elements = innerCache.Where(x => x.Key.ToString() == key);

				return elements.Any() ? elements.First().Value : throw new KeyNotFoundException("The element was not found");				
			}
			finally
			{
				cacheLock.ExitReadLock();
			}
		}

		public double ReadBalance()
		{
			cacheLock.EnterReadLock();
			try
			{
				return balance;
			}
			finally
			{
				cacheLock.ExitReadLock();
			}
		}

		public IList<Transaction> ReadAll()
		{
			cacheLock.EnterReadLock();
			try
			{
				return innerCache.Select(x => x.Value).OrderByDescending(x => x.EffectiveDate).ToList();
			}
			finally
			{
				cacheLock.ExitReadLock();
			}
		}			

		public bool AddWithTimeout(Transaction value, int timeout)
		{
			if (cacheLock.TryEnterWriteLock(timeout))
			{
				try
				{
					if (!IsAValidOperation(value))
						return false;
					innerCache.Add(value.UUID, value);
					balance += (value.Type == TransactionType.Credit ? value.Amount : (-value.Amount));
				}
				catch {
					return false;
				}
				finally
				{
					cacheLock.ExitWriteLock();
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool IsAValidOperation(Transaction value)
		{
			if (value.Type == TransactionType.Debit && value.Amount > balance)
				return false;
			else
				return true;				
		}

		~SynchronizedCache()
		{
			if (cacheLock != null) cacheLock.Dispose();
		}
	}
}
