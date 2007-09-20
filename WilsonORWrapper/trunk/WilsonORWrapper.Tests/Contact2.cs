using System;
using System.ComponentModel;
using System.Collections;
using WilsonORWrapper.Attributes;
using WilsonORWrapper.Entities;

namespace WilsonORWrapper.Tests
{
	public class Contact2 : EntityBase<Contact2>
	{
		private string _id;
		private string _fname;
		private string _lname;

		public string ID
		{
			get { return _id; }
			set { _id = value; }
		}
		public string FirstName
		{
			get { return _fname; }
			set { _fname = value; }
		}
		public string LastName
		{
			get { return _lname; }
			set { _lname = value; }
		}
	}
}
