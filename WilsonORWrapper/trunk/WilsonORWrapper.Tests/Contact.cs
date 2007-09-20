using System;
using System.ComponentModel;
using System.Collections;
using WilsonORWrapper.Attributes;
using WilsonORWrapper.Entities;

namespace WilsonORWrapper.Tests
{
	public class Contact : EntityBase<Contact>, IIdentifiable
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

		#region IIdentifiable
		public IIdentity GetIdentity()
		{
			return new ContactIdentity(_id);
		}
		#endregion
	}

	public class ContactIdentity : IdentityBase
	{
		public readonly string _id;
		private readonly string[] IDENTITY_KEYS = new string[] { "Contact.ID" };

		public ContactIdentity(string id)
		{
			_id = id;
		}
		public override string[] Keys
		{
			get { return IDENTITY_KEYS; }
		}
		public override DictionaryEntry[] GetIdentityEntries()
		{
			DictionaryEntry[] entries = new DictionaryEntry[1];
			entries[0] = new DictionaryEntry("Contact.ID", _id);

			return entries;
		}
	}
}
