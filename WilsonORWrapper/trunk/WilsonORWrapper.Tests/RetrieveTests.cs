using System;
using NUnit.Framework;
using WilsonORWrapper.Services;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class RetrieveTests 
	{
		private Contact _newcontact;
		private Contact _existingcontact;

		[SetUp]
		public void SetUp()
		{
			_newcontact = new Contact();
			_existingcontact = Data<Contact>.RetrieveFirst();
		}

		[Test]
		public void NonEntityStateTests()
		{
			Assert.IsFalse(Data<Contact>.IsEntity(_newcontact));
			Assert.IsFalse(Data<Contact>.IsNew(_newcontact));
			Assert.IsFalse(Data<Contact>.IsChanged(_newcontact));
			Assert.IsFalse(Data<Contact>.IsDeleted(_newcontact));
		}
	}
}
