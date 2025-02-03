using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    /// <summary>
    /// Represents an attached document data container (DC) that holds the details
    /// of a document related to an item, including its ID, file path, and update information.
    /// </summary>
    public class AttachedDocDC
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attached document.
        /// </summary>
        public int AttachedDocID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the item associated with the attached document.
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// Gets or sets the file path where the document is stored.
        /// </summary>
        public string DocFilepath { get; set; }

        /// <summary>
        /// Gets or sets the username of the person who last updated the document.
        /// </summary>
        public string last_updated_by { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the document was last updated.
        /// </summary>
        public DateTime last_updated_on { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachedDocDC"/> class with specific values.
        /// </summary>
        /// <param name="AttachedDocID_"> The unique identifier of the attached document. </param>
        /// <param name="ItemID_"> The unique identifier of the associated item. </param>
        /// <param name="DocFilepath_"> The file path of the document. </param>
        /// <param name="last_updated_by_"> The username of the person who last updated the document. </param>
        /// <param name="last_updated_on_"> The date and time when the document was last updated. </param>
        public AttachedDocDC(int AttachedDocID_, int ItemID_, string DocFilepath_, string last_updated_by_, DateTime last_updated_on_)
        {
            // Initialize the properties with the provided values.
            this.AttachedDocID = AttachedDocID_;
            this.ItemID = ItemID_;
            this.DocFilepath = DocFilepath_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        /// <summary>
        /// Initializes a new instace of the <see cref="AttachedDocDC"/> class with default values.
        /// </summary>
        public AttachedDocDC() { }

    }
}
