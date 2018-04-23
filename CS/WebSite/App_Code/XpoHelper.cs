using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

/// <summary>
/// Summary description for XpoHelper
/// </summary>
public static class XpoHelper {
    static XpoHelper() {
        CreateDefaultObjects();
    }

    public static Session GetNewSession() {
        return new Session(DataLayer);
    }

    public static UnitOfWork GetNewUnitOfWork() {
        return new UnitOfWork(DataLayer);
    }

    private readonly static object lockObject = new object();

    static IDataLayer fDataLayer;
    static IDataLayer DataLayer {
        get {
            if (fDataLayer == null) {
                lock (lockObject) {
                    fDataLayer = GetDataLayer();
                }
            }
            return fDataLayer;
        }
    }

    private static IDataLayer GetDataLayer() {
        XpoDefault.Session = null;

        InMemoryDataStore ds = new InMemoryDataStore();
        XPDictionary dict = new ReflectionDictionary();
        dict.GetDataStoreSchema(typeof(MyObject).Assembly);

        return new ThreadSafeDataLayer(dict, ds);
    }

    static void CreateDefaultObjects() {
        using (UnitOfWork uow = GetNewUnitOfWork()) {
            MyObject parent1 = new MyObject(uow);
            parent1.Text = "Nokia";

            MyObject parent2 = new MyObject(uow);
            parent2.Text = "Samsung";

            MyObject child21 = new MyObject(uow);
            child21.Text = "Star";

            MyObject child11 = new MyObject(uow);
            child11.Text = "N91";
            child11.Parent = parent1;

            MyObject child12 = new MyObject(uow);
            child12.Text = "N8";
            child12.Parent = parent1;

            MyObject child22 = new MyObject(uow);
            child22.Text = "Corby9";
            child22.Parent = parent2;

            uow.CommitChanges();
        }
    }
}