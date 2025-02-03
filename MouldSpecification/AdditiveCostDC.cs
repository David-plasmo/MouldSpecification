using System;

namespace MouldSpecification
{
    /// <summary>
    /// Represents the data structure for an additive cost, including all relevant details.
    /// </summary>
    public class AdditiveCostDC
    {
        /// <summary>
        /// Gets or sets the unique identifier for the additive.
        /// </summary>
        public int AdditiveID { get; set; }

        /// <summary>
        /// Gets or sets the name of the additive.
        /// </summary>
        public string Additive { get; set; }

        /// <summary>
        /// Gets or sets the code associated with the additive.
        /// </summary>
        public string AdditiveCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the additive.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the additive.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the cost per kilogram of the additive.
        /// </summary>
        public decimal CostPerKg { get; set; }

        /// <summary>
        /// Gets or sets additional comments or notes about the additive.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the supplier of the additive.
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who last updated the additive record.
        /// </summary>
        public string last_updated_by { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the last update made to the additive record.
        /// </summary>
        public DateTime last_updated_on { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditiveCostDC"/> class with specified parameters.
        /// </summary>
        /// <param name="AdditiveID_"> The unique identifier for the additive. </param>
        /// <param name="Additive_"> The name of the additive. </param>
        /// <param name="AdditiveCode_"> The code associated with the additive. </param>
        /// <param name="Type_"> The type of the additive. </param>
        /// <param name="Description_"> The description of the additive. </param>
        /// <param name="CostPerKg_"> The cost per kilogram of the additive. </param>
        /// <param name="Comment_"> Additional comments or notes about the additive. </param>
        /// <param name="Supplier_"> The supplier of the additive. </param>
        /// <param name="last_updated_by_"> The user who last updated the additive record. </param>
        /// <param name="last_updated_on_"> The timestamp of the last update made to the additive record. </param>
        public AdditiveCostDC(int AdditiveID_, string Additive_, string AdditiveCode_, string Type_, string Description_, decimal CostPerKg_, string Comment_, string Supplier_, string last_updated_by_, DateTime last_updated_on_)
        {
            // Initialize the properties with the parameter values.
            this.AdditiveID = AdditiveID_;
            this.Additive = Additive_;
            this.AdditiveCode = AdditiveCode_;
            this.Type = Type_;
            this.Description = Description_;
            this.CostPerKg = CostPerKg_;
            this.Comment = Comment_;
            this.Supplier = Supplier_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditiveCostDC"/> class.
        /// </summary>
        public AdditiveCostDC() { }

    }
}
