using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Infra.Base;

namespace LearningHub.Infra.Repository;

public class RoleRepository:BaseRepository,IRoleRepository
{

    public RoleRepository(IDbContext dbContext):base(dbContext)
    {}
    public async Task<List<RoleDto>> GetRoles()
    {
        using var connection = Connection; 
        const  string sql = "SELECT * FROM Roles";
        var result = await connection.QueryAsync<RoleDto>(sql);
        return  result.ToList(); 
    }
}