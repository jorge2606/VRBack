using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using StoresProcedures.IStoresProcedures;
using VR.Dto;

namespace StoresProcedures.StoresProcedures
{
    public class SolicitationProcedure : ISolicitationProcedure
    {
        private readonly string ConnectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=VR;Integrated Security = True;Trusted_Connection=True;MultipleActiveResultSets=true";
        public ServiceResult<List<AllSolicitationSubsidyDto>> Get_agents_solicitation_by_supervisor(
            Guid Supervisor, Guid AgentId, FilterSolicitationSubsidyDto filters)
        {
            using (var connection = new SqlConnection(ConnectionString) )
            {
                connection.Open();
                var item = new List<AllSolicitationSubsidyDto>();
                var pageIndex = filters.Page == 0 ? 1 : filters.Page;

                using (var command = new SqlCommand("exec get_agents_solicitation_by_supervisor " +
                    "@SupervisorId = '" + Supervisor +"' , " +
                    "@AgentId = '" +AgentId+"' , " +
                    "@PageSize = '"+1+ "' ," +
                    "@PageIndex = '" + pageIndex  + "' , "+
                    "@FirstName = '" + filters.FirstName + "' , " +
                    "@LastName = '" + filters.LastName + "' , " +
                    "@Dni = '" + filters.Dni + "' , " +
                    "@SortBy = 'FIRSTNAME ASC' ", connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            item.Add(
                                new AllSolicitationSubsidyDto()
                                {
                                    Id = dataReader.GetGuid(0),
                                    FullName = dataReader.GetString(1),
                                    CreateDate = dataReader.GetDateTime(2),
                                    Motive = dataReader.GetString(3),
                                    Localities = dataReader.GetString(4),
                                    Total = dataReader.GetDecimal(5),
                                    State = dataReader.GetString(6)
                                }
                            );
                        }
                }
                    
                }
                connection.Close();
                return new ServiceResult<List<AllSolicitationSubsidyDto>>(item); 
            }
            
        }




    }
}
