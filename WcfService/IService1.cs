using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IService {
    [OperationContract]
    CustomerData Get();

    [OperationContract]
    void Insert(string name, string country);

    [OperationContract]
    void Update(int customerId, string name, string country);

    [OperationContract]
    void Delete(int customerId);
}

[DataContract]
public class CustomerData {
    public CustomerData() {
        this.CustomersTable = new DataTable("CustomersData");
    }

    [DataMember]
    public DataTable CustomersTable { get; set; }
}