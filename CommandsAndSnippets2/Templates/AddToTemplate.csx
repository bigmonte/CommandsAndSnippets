/// <summary>
/// Adds a new {entityTypeName} and returns a projection of type {returnTypeFullName}.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="{toCreateVarName}">Projection data of {entityTypeName}</param>
/// <returns>The added data.</returns>
public static (bool, {returnTypeFullName}) Add{entityTypeName}(this {contextFullName} dbContext, {createTypeFullName} {toCreateVarName}) 
{
    var {newEntityVarName} = new {entityTypeFullName}({toCreateVarName});
    dbContext.{propertyName}.Add({newEntityVarName});  
    var success = dbContext.SaveChanges() >= 0;
    return (success, new {returnTypeFullName}({newEntityVarName}));
} 
