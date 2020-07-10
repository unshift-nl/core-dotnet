﻿using System;
using MessagePack;

namespace Runly
{
	/// <summary>
	/// Records the outcome of a method executed during the run of a job.
	/// </summary>
	public class MethodOutcome
	{
		Exception ex;

		/// <summary>
		/// The method this <see cref="MethodOutcome"/> represents.
		/// </summary>
		public JobMethod Method { get; private set; }

		/// <summary>
		/// True when the method completed without throwing an exception.
		/// </summary>
		[IgnoreMember]
		public bool IsSuccessful => Error == null;

		/// <summary>
		/// The exception thrown by the method, if any.
		/// </summary>
		[IgnoreMember]
		public Exception Exception
		{
			get => ex;
			private set
			{
				ex = value;
				Error = value != null ? new Error(value) : null;
			}
		}

		/// <summary>
		/// The error generated by the method, if any.
		/// </summary>
		public Error Error { get; private set; }

		/// <summary>
		/// The time the method took to execute.
		/// </summary>
		public TimeSpan Duration { get; private set; }

		/// <summary>
		/// Initializes a new <see cref="MethodOutcome"/>.
		/// </summary>
		public MethodOutcome(JobMethod method, TimeSpan duration, Exception ex)
		{
			Method = method;
			Exception = ex;
			Duration = duration;
		}

		/// <summary>
		/// Initializes a new <see cref="MethodOutcome"/>.
		/// </summary>
		[SerializationConstructor]
		public MethodOutcome(JobMethod method, TimeSpan duration, Error error)
		{
			Method = method;
			Error = error;
			Duration = duration;
		}

		/// <summary>
		/// Returns a string description of this <see cref="MethodOutcome"/>.
		/// </summary>
		public override string ToString()
		{
			if (IsSuccessful)
				return $"Completed successfuly in {Duration}";

			return $"Completed with error in {Duration}";
		}
	}
}
