using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Team;
using tibs.stem.Teamss.Dto;
using tibs.stem.TeamDetails;
using System.Data;
using System.Data.SqlClient;

namespace tibs.stem.Teamss
{
    public class TeamAppService : stemAppServiceBase, ITeamAppService
    {
        private readonly IRepository<Teams> _TeamsRepository;
        private readonly IRepository<TeamDetail> _TeamDetailRepository;

        public TeamAppService(IRepository<Teams> TeamsRepository, IRepository<TeamDetail> TeamDetailRepository)
        {
            _TeamsRepository = TeamsRepository;
            _TeamDetailRepository = TeamDetailRepository;

        }
        public ListResultDto<TeamListDto> GetTeam(GetTeamInput input)
        {
            var query = _TeamsRepository.GetAll()
                   .WhereIf(
                   !input.Filter.IsNullOrWhiteSpace(),
                   u =>
                       u.Name.Contains(input.Filter) ||
                       u.SalesManagerId.ToString().Contains(input.Filter) ||
                       u.DepartmentId.ToString().Contains(input.Filter) ||
                       u.Id.ToString().Contains(input.Filter));
            
            var Teams = (from a in query
                           select new TeamListDto
                           {
                               Id = a.Id,
                               Name = a.Name,
                               DepartmentId = a.DepartmentId,
                               DepartmentName = a.Department.DepatmentName,
                               SalesManagerId = a.SalesManagerId,
                               SalesManager = a.SalesManager.Name
                              
                           }).ToList();

            return new ListResultDto<TeamListDto>(Teams.MapTo<List<TeamListDto>>());
        }

        public async Task<GetTeam> GetTeamForEdit(NullableIdDto input)
        {
            var output = new GetTeam
            {
            };

            var team = _TeamsRepository.GetAll().Where(p => p.Id == input.Id);

            var Teams = (from a in team
                         select new TeamListDto
                         {
                             Id = a.Id,
                             Name = a.Name,
                             DepartmentId = a.DepartmentId,
                             DepartmentName = a.Department.DepatmentName,
                             SalesManagerId = a.SalesManagerId,
                             SalesManager = a.SalesManager.Name

                         }).FirstOrDefault();

            output.Teams = Teams.MapTo<TeamListDto>();
            return output;
        }

        public async Task CreateOrUpdateTeam(CreateTeamInput input)
        {
            if (input.Id != 0)
            {
                await UpdateTeam(input);
            }
            else
            {
                await CreateTeam(input);
            }
        }

        public async Task CreateTeam(CreateTeamInput input)
        {
            var team = input.MapTo<Teams>();
            var val = _TeamsRepository
             .GetAll().Where(p => p.Name == input.Name && p.SalesManagerId == input.SalesManagerId ).FirstOrDefault();

            if (val == null)
            {
                await _TeamsRepository.InsertAsync(team);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Team '" + input.Name + "'...");
            }
        }

        public async Task UpdateTeam(CreateTeamInput input)
        {
            var team = input.MapTo<Teams>();
            var val = _TeamsRepository
             .GetAll().Where(p => p.Name == input.Name && p.SalesManagerId == input.SalesManagerId && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _TeamsRepository.UpdateAsync(team);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Team '" + input.Name + "'...");
            }
        }

        public async Task DeleteTeam(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 32);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {

                    da.Fill(ds);
                }

            }

            if (input.Id > 0)
            {
                var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
                if (results.Count() > 0)
                {
                    throw new UserFriendlyException("Ooops!", "Team cannot be deleted '");
                }
                else
                {
                    await _TeamsRepository.DeleteAsync(input.Id);
                }
            }
        }

        public ListResultDto<TeamDetailList> GetTeamDetail(GetTeamDetailInput input)
        {
            var query = _TeamDetailRepository.GetAll().Where(q => q.TeamId == input.TeamId);

            var TeamDetail = (from a in query
                              select new TeamDetailList
                              {
                                  Id = a.Id,
                                  TeamId = a.TeamId,
                                  TeamName = a.TeamId != null ? a.Team.Name : "",
                                  SalesmanId = a.SalesmanId,
                                  Salesman = a.Salesman.Name,

                              }).ToList();

            return new ListResultDto<TeamDetailList>(TeamDetail.MapTo<List<TeamDetailList>>());
        }
        public async Task CreateTeamDetail(CreateTeamDetailInput input)
        {
            var TeamDetail = input.MapTo<TeamDetail>();
            var val = _TeamDetailRepository
             .GetAll().Where(p => p.SalesmanId == input.SalesmanId).FirstOrDefault();

            if (val == null)
            {
                await _TeamDetailRepository.InsertAsync(TeamDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in TeamDetail...");
            }
        }
        public async Task DeleteTeamDetail(EntityDto input)
        {
            if(input.Id > 0)
            {
                await _TeamDetailRepository.DeleteAsync(input.Id);
            }
        }
    }
}

