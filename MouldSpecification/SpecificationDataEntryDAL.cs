using DataService;
using System;
using System.Data;
using System.Windows.Forms;

namespace MouldSpecification
{
    public class SpecificationDataEntryDAL : DataAccessBase
    {
        public DataSet GetCustomerIndex(string productType)
        {
            try
            {
                DataSet ds = ExecuteDataSet("SelectCustomerIndex",CreateParameter("@ProductType", SqlDbType.VarChar, productType));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetProductIndex(string productType)
        {
            try
            {
                DataSet ds = ExecuteDataSet("SelectMAN_ItemIndex", CreateParameter("@ProductType", SqlDbType.VarChar, productType));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet BuildFormDataSet()
        {
            try
            {
                //create output dataset and add tables                
                DataSet dsOut = new DataSet();

                // create product table               
                DataTable dt = ExecuteDataSet("SelectMAN_Item").Tables[0];
                DataTable manItems = dt.Copy();
                manItems.TableName = "MAN_Items";
                dsOut.Tables.Add(manItems);

                // create customer table
                dt = ExecuteDataSet("SelectCustomer").Tables[0];
                DataTable customer = dt.Copy();
                customer.TableName = "Customer";
                dsOut.Tables.Add(customer);

                // create customer - product table 
                dt = ExecuteDataSet("SelectCustomerProduct").Tables[0];
                DataTable custProduct = dt.Copy();
                custProduct.TableName = "CustomerProduct";
                dsOut.Tables.Add(custProduct);

                //create Specification table
                dt = ExecuteDataSet("SelectInjectionMouldSpecification").Tables[0];
                DataTable mouldSpec = dt.Copy();
                mouldSpec.TableName = "InjectionMouldSpecification";
                dsOut.Tables.Add(mouldSpec);

                //create Material composition table
                dt = ExecuteDataSet("SelectMaterialComp").Tables[0];
                DataTable materialComp = dt.Copy();
                materialComp.TableName = "MaterialComp";
                dsOut.Tables.Add(materialComp);

                //create Material table
                dt = ExecuteDataSet("GetMaterial").Tables[0];
                DataTable material = dt.Copy();
                material.TableName = "Material";
                dsOut.Tables.Add(material);

                //create Masterbatch composition table
                dt = ExecuteDataSet("SelectMasterBatchComp").Tables[0];
                DataTable mbComp = dt.Copy();
                mbComp.TableName = "MasterBatchComp";
                dsOut.Tables.Add(mbComp);

                //create Masterbatch colour and code table
                dt = ExecuteDataSet("SelectMasterBatch").Tables[0];
                DataTable masterbatch = dt.Copy();
                masterbatch.TableName = "MasterBatch";
                dsOut.Tables.Add(masterbatch);

                //create Additive table
                dt = ExecuteDataSet("SelectAdditive").Tables[0];
                DataTable additive = dt.Copy();
                additive.TableName = "Additive";
                dsOut.Tables.Add(additive);

                //create Material Grade table
                dt = ExecuteDataSet("SelectMaterialGrade").Tables[0];
                DataTable materialGrade = dt.Copy();
                materialGrade.TableName = "MaterialGrade";
                dsOut.Tables.Add(materialGrade);

                //create Machine preference table
                dt = ExecuteDataSet("SelectMachinePref").Tables[0];
                DataTable machPref = dt.Copy();
                machPref.TableName = "MachinePref";
                dsOut.Tables.Add(machPref);

                //create quality control table
                dt = ExecuteDataSet("SelectQualityControl").Tables[0];
                DataTable qc = dt.Copy();
                qc.TableName = "QualityControl";
                dsOut.Tables.Add(qc);

                //Create ProductGrade (aka ProductCategory)
                dt = ExecuteDataSet("SelectProductGrade").Tables[0];
                DataTable productGrade = dt.Copy();
                productGrade.TableName = "ProductGrade";
                dsOut.Tables.Add(productGrade);

                // Create lookup table for MaterialGrade dropdown lists
                dt = ExecuteDataSet("LookupMaterialGrade",
                    CreateParameter("@MachineType", SqlDbType.VarChar, "IM")). Tables[0];
                DataTable lookupMaterialGrade = dt.Copy();
                lookupMaterialGrade.TableName = "LookupMaterialGrade";
                dsOut.Tables.Add(lookupMaterialGrade);

                //create Packaging table
                dt = new PackagingDAL().SelectPackaging(null, null).Tables[0];
                DataTable packaging = dt.Copy();
                packaging.TableName = "Packaging";
                dsOut.Tables.Add(packaging);

                ////define table relationships - see useful reference below
                ///https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/dataset-datatable-dataview/navigating-datarelations
                ///
                dsOut.Relations.Add(new DataRelation("ItemMould", manItems.Columns["ItemID"], mouldSpec.Columns["ItemID"], false));
                //dsOut.Relations.Add(new DataRelation("CustProducts", custProduct.Columns["ItemID"], manItems.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("ItemMaterialComp", manItems.Columns["ItemID"], materialComp.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("ItemMasterBatchComp", manItems.Columns["ItemID"], mbComp.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("MaterialGradeComp", materialGrade.Columns["MaterialGradeID"], materialComp.Columns["MaterialGradeID"], false));
                //dsOut.Relations.Add(new DataRelation("MaterialMaterialGrade", material.Columns["MaterialID"], materialGrade.Columns["MaterialID"], false));
                //dsOut.Relations.Add(new DataRelation("MaterialGradeMaterial",materialGrade.Columns["MaterialID"], material.Columns["MaterialID"], false));
                dsOut.Relations.Add(new DataRelation("MachinePref", manItems.Columns["ItemID"], machPref.Columns["ItemID"], false));
                //dsOut.Relations.Add(new DataRelation("ItemMB", manItems.Columns["ItemID"], mbComp.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("MBMBcomp", masterbatch.Columns["MBID"], mbComp.Columns["MBID"], false));
                dsOut.Relations.Add(new DataRelation("AddMBComp", additive.Columns["AdditiveID"], mbComp.Columns["AdditiveID"], false));
                dsOut.Relations.Add(new DataRelation("ItemQC", manItems.Columns["ItemID"], qc.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("ProductGradeItem", productGrade.Columns["GradeID"], manItems.Columns["GradeID"], false));
                
                return dsOut;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet BuildPackagingDataSet()
        {
            try
            {
                //create output dataset and add tables                
                DataSet dsOut = new DataSet();

                //create product index
                DataTable dt = ExecuteDataSet("SelectMAN_ItemIndex").Tables[0];
                DataTable product = dt.Copy();
                product.TableName = "Product";
                dsOut.Tables.Add(product);

                // create product table               
                dt = ExecuteDataSet("SelectMAN_Item").Tables[0];
                DataTable manItems = dt.Copy();
                manItems.TableName = "MAN_Items";
                dsOut.Tables.Add(manItems);

                // create customer table
                dt = ExecuteDataSet("SelectCustomer").Tables[0];
                DataTable customer = dt.Copy();
                customer.TableName = "Customer";
                dsOut.Tables.Add(customer);

                // create customer - product table 
                dt = ExecuteDataSet("SelectCustomerProduct").Tables[0];
                DataTable custProduct = dt.Copy();
                custProduct.TableName = "CustomerProduct";
                dsOut.Tables.Add(custProduct);


                //create quality control table
                dt = ExecuteDataSet("SelectQualityControl").Tables[0];
                DataTable qc = dt.Copy();
                qc.TableName = "QualityControl";
                dsOut.Tables.Add(qc);

                //Create ProductGrade (aka ProductCategory)
                dt = ExecuteDataSet("SelectProductGrade").Tables[0];
                DataTable productGrade = dt.Copy();
                productGrade.TableName = "ProductGrade";
                dsOut.Tables.Add(productGrade);

                //create Packaging table
                dt = new PackagingDAL().SelectPackaging(null, null).Tables[0];
                DataTable packaging = dt.Copy();
                packaging.TableName = "Packaging";
                dsOut.Tables.Add(packaging);

                //create CartonPackaging table
                dt = ExecuteDataSet("SelectCartonPackaging").Tables[0];
                DataTable ctnpackaging = dt.Copy();
                ctnpackaging.TableName = "CartonPackaging";
                dsOut.Tables.Add(ctnpackaging);

                //create Pallet table
                dt = ExecuteDataSet("SelectPallet").Tables[0];
                DataTable pallet = dt.Copy();
                pallet.TableName = "Pallet";
                dsOut.Tables.Add(pallet);

                //create Packing style table
                dt = ExecuteDataSet("SelectPackingStyle").Tables[0];
                DataTable packingstyle = dt.Copy();
                packingstyle.TableName = "PackingStyle";
                dsOut.Tables.Add(packingstyle);

                //create Wrapping type table
                dt = ExecuteDataSet("SelectWrapping").Tables[0];
                DataTable wrapping = dt.Copy();
                wrapping.TableName = "Wrapping";
                dsOut.Tables.Add(wrapping);

                //create Barcode label table
                dt = ExecuteDataSet("SelectBarcodeLabel").Tables[0];
                DataTable barcodelabel = dt.Copy();
                barcodelabel.TableName = "BarcodeLabel";
                dsOut.Tables.Add(barcodelabel);

                //create LabelInnerBag table
                dt = ExecuteDataSet("SelectLabelInnerBag").Tables[0];
                DataTable labelinnerbag = dt.Copy();
                labelinnerbag.TableName = "LabelInnerBag";
                dsOut.Tables.Add(labelinnerbag);

                //create PackagingImage table
                dt = ExecuteDataSet("SelectPackingImage").Tables[0];
                DataTable packingImage = dt.Copy();
                packingImage.TableName = "PackingImage";
                dsOut.Tables.Add(packingImage);

                //create PackingInstruction table
                dt = ExecuteDataSet("SelectPackingInstruction").Tables[0];
                DataTable packingInstruction = dt.Copy();
                packingInstruction.TableName = "PackingInstruction";
                dsOut.Tables.Add(packingInstruction);

                //create ReworkInstruction table
                dt = ExecuteDataSet("SelectReworkInstruction").Tables[0];
                DataTable reworkInstruction = dt.Copy();
                reworkInstruction.TableName = "ReworkInstruction";
                dsOut.Tables.Add(reworkInstruction);

                //create AssemblyInstruction table
                dt = ExecuteDataSet("SelectAssemblyInstruction_Pivot").Tables[0];
                DataTable assemblyInstruction = dt.Copy();
                assemblyInstruction.TableName = "AssemblyInstruction";
                dsOut.Tables.Add(assemblyInstruction);


                ////define table relationships - see useful reference below
                ///https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/dataset-datatable-dataview/navigating-datarelations
                ///
                //dsOut.Relations.Add(new DataRelation("PackagingItem", packaging.Columns["ItemID"], manItems.Columns["ItemID"], false));
                //dsOut.Relations.Add(new DataRelation("CustProducts", custProduct.Columns["ItemID"], manItems.Columns["ItemID"], true));
                //dsOut.Relations.Add(new DataRelation("ItemPackaging", manItems.Columns["ItemID"], packaging.Columns["ItemID"],  false));
                dsOut.Relations.Add(new DataRelation("ProductGradeItem", productGrade.Columns["GradeID"], manItems.Columns["GradeID"], false));
                dsOut.Relations.Add(new DataRelation("CtnPackagingPackaging", ctnpackaging.Columns["CtnID"], packaging.Columns["CtnID"], false));
                dsOut.Relations.Add(new DataRelation("PalletPackaging", pallet.Columns["PalletID"], packaging.Columns["PalletID"], false));
                dsOut.Relations.Add(new DataRelation("ItemPackingImage", manItems.Columns["ItemID"], packingImage.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("ItemPackingInstruction", manItems.Columns["ItemID"], packingInstruction.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("ItemReworkInstruction", manItems.Columns["ItemID"], reworkInstruction.Columns["ItemID"], false));
                dsOut.Relations.Add(new DataRelation("ItemAssemblyInstruction", manItems.Columns["ItemID"], assemblyInstruction.Columns["ItemID1"], false));
                //dsOut.Relations.Add(new DataRelation("ItemAssemblyImage", manItems.Columns["ItemID"], assemblyImage.Columns["ItemID"], false));

                /*
                ds.Relations.AddRange(new System.Data.DataRelation[] 
                {   
                    new DataRelation("ItemMould", manItems.Columns["ItemID"], mouldSpec.Columns["ItemID"],true),
                    new DataRelation("CustProducts", custProducts.Columns["ItemID"], manItems.Columns["ItemID"],true),
                    //new DataRelation("MouldCust", customer.Columns["CustomerID"], mouldSpec.Columns["CustomerID"]), 
                    new DataRelation("ItemMaterialComp", manItems.Columns["ItemID"], materialComp.Columns["ItemID"],false),
                    new DataRelation("MaterialGradeComp", materialGrade.Columns["MaterialGradeID"], materialComp.Columns["MaterialGradeID"],true),
                    new DataRelation("MaterialMaterialGrade", material.Columns["MaterialID"], materialGrade.Columns["MaterialID"],false),
                    new DataRelation("MachinePref", manItems.Columns["ItemID"], machPref.Columns["ItemID"],true),
                    new DataRelation("ItemMB", manItems.Columns["ItemID"], mbComp.Columns["ItemID"], true),
                    new DataRelation("MBMBcomp", masterbatch.Columns["MBID"], mbComp.Columns["MBID"], true),                    
                    new DataRelation("AddMBComp", additive.Columns["AdditiveID"], mbComp.Columns["AdditiveID"],false),
                    new DataRelation("ItemQC", manItems.Columns["ItemID"], qc.Columns["ItemID"],false)

                    //bsMaterialGradeComp.DataSource = dsIMSpecificationForm.Tables["MaterialComp"].ParentRelations["MaterialGradeComp"].ParentTable;
                });
                */
                return dsOut;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet BuildQCDataset()
        {
            try
            {
                //create output dataset and add tables                
                DataSet dsOut = new DataSet();

                //create product index
                DataTable dt = ExecuteDataSet("SelectMAN_ItemIndex").Tables[0];
                DataTable product = dt.Copy();
                product.TableName = "Product";
                dsOut.Tables.Add(product);

                // create product table               
                dt = ExecuteDataSet("SelectMAN_Item").Tables[0];
                DataTable manItems = dt.Copy();
                manItems.TableName = "MAN_Items";
                dsOut.Tables.Add(manItems);

                // create customer table
                dt = ExecuteDataSet("SelectCustomer").Tables[0];
                DataTable customer = dt.Copy();
                customer.TableName = "Customer";
                dsOut.Tables.Add(customer);

                // create customer - product table 
                dt = ExecuteDataSet("SelectCustomerProduct").Tables[0];
                DataTable custProduct = dt.Copy();
                custProduct.TableName = "CustomerProduct";
                dsOut.Tables.Add(custProduct);

                //Create ProductGrade (aka ProductCategory)
                dt = ExecuteDataSet("SelectProductGrade").Tables[0];
                DataTable productGrade = dt.Copy();
                productGrade.TableName = "ProductGrade";
                dsOut.Tables.Add(productGrade);

                //Create QCInstruction table - note pivot for datagridview
                dt = ExecuteDataSet("SelectQCInstruction_Pivot").Tables[0];
                DataTable qcInstruction = dt.Copy();
                qcInstruction.TableName = "QCInstruction";
                dsOut.Tables.Add(qcInstruction);

                //dsOut.Relations.Add(new DataRelation("CustProducts", custProduct.Columns["ItemID"], manItems.Columns["ItemID"], true));
                dsOut.Relations.Add(new DataRelation("ProductGradeItem", productGrade.Columns["GradeID"], manItems.Columns["GradeID"], false));
                dsOut.Relations.Add(new DataRelation("ItemQCInstruction", manItems.Columns["ItemID"], qcInstruction.Columns["ItemID1"]));

                return dsOut;
            }
            catch (Exception ex) { return null; }
        }

        public DataSet BuildAttachedDocDataset()
        {
            try
            {
                //create output dataset and add tables                
                DataSet dsOut = new DataSet();

                //create product index
                DataTable dt = ExecuteDataSet("SelectMAN_ItemIndex").Tables[0];
                DataTable product = dt.Copy();
                product.TableName = "Product";
                dsOut.Tables.Add(product);

                // create product table               
                dt = ExecuteDataSet("SelectMAN_Item").Tables[0];
                DataTable manItems = dt.Copy();
                manItems.TableName = "MAN_Items";
                dsOut.Tables.Add(manItems);

                // create customer table
                dt = ExecuteDataSet("SelectCustomer").Tables[0];
                DataTable customer = dt.Copy();
                customer.TableName = "Customer";
                dsOut.Tables.Add(customer);

                // create customer - product table 
                dt = ExecuteDataSet("SelectCustomerProduct").Tables[0];
                DataTable custProduct = dt.Copy();
                custProduct.TableName = "CustomerProduct";
                dsOut.Tables.Add(custProduct);

                //Create ProductGrade (aka ProductCategory)
                dt = ExecuteDataSet("SelectProductGrade").Tables[0];
                DataTable productGrade = dt.Copy();
                productGrade.TableName = "ProductGrade";
                dsOut.Tables.Add(productGrade);

                //Create QCInstruction table - note pivot for datagridview
                dt = ExecuteDataSet("SelectAttachedDoc").Tables[0];
                DataTable attachedDoc = dt.Copy();
                attachedDoc.TableName = "AttachedDoc";
                dsOut.Tables.Add(attachedDoc);

                //dsOut.Relations.Add(new DataRelation("CustProducts", custProduct.Columns["ItemID"], manItems.Columns["ItemID"], true));
                dsOut.Relations.Add(new DataRelation("ProductGradeItem", productGrade.Columns["GradeID"], manItems.Columns["GradeID"], false));
                dsOut.Relations.Add(new DataRelation("ItemAttachedDoc", manItems.Columns["ItemID"], attachedDoc.Columns["ItemID"], false));
                return dsOut;
            }
            catch (Exception ex) { return null; }
        }



        public DataTable GetDataTableFromDataReader(IDataReader dataReader)
        {
            DataTable schemaTable = dataReader.GetSchemaTable();
            DataTable resultTable = new DataTable();

            foreach (DataRow dataRow in schemaTable.Rows)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = dataRow["ColumnName"].ToString();
                dataColumn.DataType = Type.GetType(dataRow["DataType"].ToString());
                dataColumn.ReadOnly = (bool)dataRow["IsReadOnly"];
                dataColumn.AutoIncrement = (bool)dataRow["IsAutoIncrement"];
                dataColumn.Unique = (bool)dataRow["IsUnique"];

                resultTable.Columns.Add(dataColumn);
            }

            while (dataReader.Read())
            {
                DataRow dataRow = resultTable.NewRow();
                for (int i = 0; i < resultTable.Columns.Count; i++)
                {
                    dataRow[i] = dataReader[i];
                }
                resultTable.Rows.Add(dataRow);
            }

            return resultTable;
        }
    }
}
