using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;

/// <summary>
/// Summary description for ObjectHierarchyData
/// </summary>
public class ObjectHierarchyData<T> : IHierarchyData {
    Session session;

    T obj;
    String parentProp;
    String childrenProp;

    XPClassInfo classInfo;

    public ObjectHierarchyData(Session session, T obj, String parentProp, String childrenProp) {
        this.session = session;

        this.obj = obj;
        this.parentProp = parentProp;
        this.childrenProp = childrenProp;

        classInfo = session.GetClassInfo(typeof(T));
    }

    public IHierarchicalEnumerable GetChildren() {
        XPMemberInfo memberInfo = classInfo.GetMember(childrenProp);
        XPCollection<T> children = (XPCollection<T>)memberInfo.GetValue(obj);
        /* without a sotring the order of objects might differ */
        children.Sorting.Add(new SortProperty(classInfo.KeyProperty.Name, DevExpress.Xpo.DB.SortingDirection.Ascending));

        return new ObjectCollection<T>(children, parentProp, childrenProp);
    }

    public IHierarchyData GetParent() {
        XPMemberInfo memberInfo = classInfo.GetMember(parentProp);
        T parent = (T)memberInfo.GetValue(obj);

        if (parent == null)
            return null;

        ObjectHierarchyData<T> hierarchyData = new ObjectHierarchyData<T>(session, parent, parentProp, childrenProp);

        return hierarchyData as IHierarchyData;
    }

    public bool HasChildren {
        get {
            XPMemberInfo memberInfo = classInfo.GetMember(childrenProp);
            XPCollection<T> children = (XPCollection<T>)memberInfo.GetValue(obj);

            return (children.Count > 0);
        }
    }

    public Object Item {
        get {
            return obj;
        }
    }

    public string Path {
        get {
            Object key = session.GetKeyValue(obj);

            return key.ToString();
        }
    }

    public string Type {
        get {
            return classInfo.FullName;
        }
    }
}