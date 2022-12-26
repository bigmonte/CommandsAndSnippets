/// <summary>
/// Adds a new item into {listPropertyName} from {entityTypeName}.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="{primaryKeyVarName}">The {entityTypeName} key to get</param>
/// <param name="{listItemCreateVarName}">The item to add to {listPropertyName}.</param>
/// <returns>The success of the operation, a message, and a projection of the added item. </returns>
public static (bool, string, {returnTypeFullName}) Add{listEntityTypeName}To{entityTypeName}(this {contextFullName} dbContext, 
        {primaryKeyFullName} {primaryKeyVarName}, {listEntityCreateFullName} {listItemCreateVarName})
{
    var entityQuery = from aEntity in dbContext.{propertyName}
        where aEntity.{primaryKeyPropertyName} == {primaryKeyVarName}
        let itemsInList = aEntity.{listPropertyName}
        select aEntity;
    
    var entity = entityQuery.FirstOrDefault();
    
    if (entity == null) return (false, $"{entityTypeName} Not found.", null);

    var newListItem = new {listEntityFullName}({listItemCreateVarName});
    entity.{listPropertyName}.Add(newListItem);
    var success = dbContext.SaveChanges() >= 0;

    return !success ? (false, "Error saving changes in the Database. Action: Create {listEntityTypeName} in {entityTypeName}.", null) : 
           (true, string.Empty, new {returnTypeFullName}(newListItem));
}