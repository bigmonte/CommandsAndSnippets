/// <summary>
/// Updates {entityTypeName} in the DbSet of <see cref="{contextFullName}"></see>.
/// </summary>
/// <param name="dbContext">Database context</param>
/// <param name="{updateVarName}">The data type with the add data to update {entityTypeName}</param>
/// <param name="{keyVarName}"> The primary key.</param>
/// <returns>Returns the current data.</returns>
public static (bool, {returnTypeFullName}) Update{entityTypeName}( this {contextFullName} dbContext, {updateTypeFullName} {updateVarName}, {keyTypeFullName} {keyVarName})
{
    var {existingEntityVarName} = dbContext.{propertyName}.FirstOrDefault(x => x.{keyPropertyName} == {keyVarName});
    if ({existingEntityVarName} == null) return (false, null);
    {existingEntityVarName}.Update({updateVarName});
    dbContext.{propertyName}.Update({existingEntityVarName});
    var result = dbContext.SaveChanges() >= 0;
    return (result, new {returnTypeFullName}({existingEntityVarName}));
}