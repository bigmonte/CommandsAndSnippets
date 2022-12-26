/// <summary>
/// Gets a projection of the first <see cref="{returnTypeFullName}">{listItemTypeName}</see> occurrence.
/// </summary>
/// <param name="dbContext">The <see cref="{contextFullName}">Database</see> context.</param>
/// <param name="{primaryKeyVarName}">{entityTypeName} lookup key.</param>
/// <param name="{listPrimaryKeyVarName}">{listItemTypeName} lookup key.</param>
/// <returns>A <see cref="{returnTypeFullName}">projection</see> of {listItemTypeName} </returns>
public static (bool, {returnTypeFullName}) Get{listItemTypeName}From{entityTypeName} (this {contextFullName} dbContext, {primaryKeyFullName} {primaryKeyVarName}, 
        {listPrimaryKeyFullName} {listPrimaryKeyVarName})
{
    var query =
        from mainEntity in dbContext.{propertyName}
        where mainEntity.{primaryKeyPropertyName} == {primaryKeyVarName}
        let list = mainEntity.{listPropertyName}
        from listItem in list
        where listItem.{listPrimaryKeyPropertyName} == {listPrimaryKeyVarName}
        select new {returnTypeFullName}(listItem);

    return (query.Any(), query.FirstOrDefault());
}
