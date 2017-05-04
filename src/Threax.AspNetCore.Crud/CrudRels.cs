using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Crud
{
    /// <summary>
    /// These rels are the common rels that the crud table clients expect to find. These
    /// should be aliased to your real rels on your controller for anything you want to
    /// conform to the CRUD hypermedia interface.
    /// </summary>
    public static class CrudRels
    {
        /// <summary>
        /// The name of the add (used by create) function.
        /// </summary>
        public const String Add = "Add";

        /// <summary>
        /// The name of the update function.
        /// </summary>
        public const String Update = "Update";

        /// <summary>
        /// The name of the delete function.
        /// </summary>
        public const String Delete = "Delete";
    }
}
