using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wilson.ORMapper.FluentMappings
{
    public class StoredProcedures
    {

        /// <summary>
        /// The stored procedure to use for inserts.
        /// </summary>
        public string Insert { get; set; }

        /// <summary>
        /// The stored procedure to use for updates.
        /// </summary>
        public string Update { get; set; }

        /// <summary>
        /// The stored procedure to use for deletes.
        /// </summary>
        public string Delete { get; set; }
    }
}
