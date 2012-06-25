using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace DotNetUtilityLibrary
{
	/// <summary>
	/// Summary:
	///		Wraps the BackgroundWorker class, abbreviating calls and handling
	///	common bookkeeping aspects.
	/// </summary>
	public static class BackgroundWorkerHelper
	{

		private static BackgroundWorker SetupBackgroundWorker(
			DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted,
			ProgressChangedEventHandler progressChanged,
			bool supportsCancellation)
		{
			BackgroundWorker worker = new BackgroundWorker();
			if (doWork != null)
				worker.DoWork += doWork;
			if (workCompleted != null)
				worker.RunWorkerCompleted += workCompleted;
			worker.WorkerSupportsCancellation = supportsCancellation;
			if (progressChanged != null)
			{
				worker.WorkerReportsProgress = true;
				worker.ProgressChanged += progressChanged;
			}
			return worker;
		}

		#region SetupBackgroundWorker Abbreviated Calls

		public static BackgroundWorker SetupBackgroundWorker(
			DoWorkEventHandler doWork)
		{
			return SetupBackgroundWorker(doWork, null, null, true);
		}

		public static BackgroundWorker SetupBackgroundWorker(
			DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted)
		{
			return SetupBackgroundWorker(doWork, workCompleted, null, true);
		}

		public static BackgroundWorker SetupBackgroundWorker(
			DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged)
		{
			return SetupBackgroundWorker(doWork, null, progressChanged, true);
		}

		public static BackgroundWorker SetupBackgroundWorker(
			DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged,
			RunWorkerCompletedEventHandler workCompleted)
		{
			return SetupBackgroundWorker(doWork, workCompleted, progressChanged,
				true);
		}

		#endregion SetupBackgroundWorker Abbreviated Calls

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted,
			ProgressChangedEventHandler progressChanged,
			bool supportsCancellation,
			List<object> parameters)
		{
			BackgroundWorker worker = SetupBackgroundWorker(
				doWork,
				workCompleted,
				progressChanged,
				supportsCancellation);

			worker.RunWorkerAsync(parameters);
			return worker;
		}

		#region RunAsyncWork Abreviated Calls

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork)
		{
			return RunAsyncWork(doWork, null, null, true, null);
		}

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			List<object> parameters)
		{
			return RunAsyncWork(doWork, null, null, true, parameters);
		}

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted)
		{
			return RunAsyncWork(doWork, workCompleted, null, true, null);
		}

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted,
			List<object> parameters)
		{
			return RunAsyncWork(doWork, workCompleted, null, true, parameters);
		}

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged)
		{
			return RunAsyncWork(doWork, null, progressChanged, true, null);
		}

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged,
			List<object> parameters)
		{
			return RunAsyncWork(doWork, null, progressChanged, true, parameters);
		}

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged,
			RunWorkerCompletedEventHandler workCompleted)
		{
			return RunAsyncWork(doWork, workCompleted, progressChanged, true,
				null);
		}

		public static BackgroundWorker RunAsyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted,
			ProgressChangedEventHandler progressChanged,
			List<object> parameters)
		{
			return RunAsyncWork(doWork, workCompleted, progressChanged, true,
				parameters);
		}

		#endregion RunAsyncWork Abreviated Calls

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted,
			ProgressChangedEventHandler progressChanged,
			bool supportsCancellation,
			List<object> parameters)
		{
			BackgroundWorker worker = SetupBackgroundWorker(
				doWork,
				workCompleted,
				progressChanged,
				supportsCancellation);
			AutoResetEvent resetEvent = new AutoResetEvent(false);
			worker.RunWorkerCompleted += (sender, e) => { resetEvent.Set(); };
			worker.RunWorkerAsync(parameters);
			resetEvent.WaitOne();
			return worker;
		}

		#region RunSyncWork Abreviated Calls

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork)
		{
			return RunSyncWork(doWork, null, null, true, null);
		}

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			List<object> parameters)
		{
			return RunSyncWork(doWork, null, null, true, parameters);
		}

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted)
		{
			return RunSyncWork(doWork, workCompleted, null, true, null);
		}

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted,
			List<object> parameters)
		{
			return RunSyncWork(doWork, workCompleted, null, true, parameters);
		}

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged)
		{
			return RunSyncWork(doWork, null, progressChanged, true, null);
		}

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged,
			List<object> parameters)
		{
			return RunSyncWork(doWork, null, progressChanged, true, parameters);
		}

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			ProgressChangedEventHandler progressChanged,
			RunWorkerCompletedEventHandler workCompleted)
		{
			return RunSyncWork(doWork, workCompleted, progressChanged, true,
				null);
		}

		public static BackgroundWorker RunSyncWork(DoWorkEventHandler doWork,
			RunWorkerCompletedEventHandler workCompleted,
			ProgressChangedEventHandler progressChanged,
			List<object> parameters)
		{
			return RunSyncWork(doWork, workCompleted, progressChanged, true,
				parameters);
		}

		#endregion RunSyncWork Abreviated Calls
	}
}