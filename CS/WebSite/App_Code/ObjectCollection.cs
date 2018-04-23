using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DevExpress.Xpo;
using System.Collections;

/// <summary>
/// Summary description for ObjectCollction
/// </summary>
public class ObjectCollection<T> : IHierarchicalEnumerable {
    XPCollection<T> children;
    String parentProp;
    String childrenProp;

    public ObjectCollection(XPCollection<T> children, String parentProp, String childrenProp) {
        this.children = children;
        this.parentProp = parentProp;
        this.childrenProp = childrenProp;
    }

    public IHierarchyData GetHierarchyData(object enumeratedItem) {
        ObjectHierarchyData<T> obj = new ObjectHierarchyData<T>(children.Session, (T)enumeratedItem, parentProp, childrenProp);

        return obj as IHierarchyData;
    }

    public IEnumerator GetEnumerator() {
        return (children as IEnumerable<T>).GetEnumerator();
    }
}