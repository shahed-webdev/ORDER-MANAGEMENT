using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public void CreateUser(CreateUserVM userVM)
        {
            var HierarchyID = Context.Organization_hierarchy.FirstOrDefault(h => h.Rank == userVM.Rank).HierarchyID;
            var user_Territories = new List<User_Territory>();

            foreach (var item in userVM.TerritoryIDs)
            {
                user_Territories.Add(new User_Territory() { TerritoryID = item });
            }

            var Reg = new Registration()
            {
                Name = userVM.Name,
                UserName = userVM.UserName,
                Type = "User",
                Designation = userVM.Designation,
                JoiningDate = userVM.JoiningDate,
                DOB = userVM.DOB,
                NID = userVM.NID,
                FatherName = userVM.FatherName,
                PresentAddress = userVM.PresentAddress,
                PermanentAddress = userVM.PermanentAddress,
                OfficeContact = userVM.OfficeContact,
                PersonalContact = userVM.PersonalContact,
                OfficeEmail = userVM.OfficeEmail,
                PersonalEmail = userVM.PersonalEmail,
                PS = userVM.PS,
                User = new User
                {
                    HierarchyID = HierarchyID,
                    Upper_RegistrationID = userVM.UpperRegistrationID,
                    user_Territories = user_Territories
                }
            };

            //TerritoryIDs

            //var user = new User()
            //{
            //    HierarchyID = HierarchyID,
            //    Registration = Reg,
            //    Upper_RegistrationID = userVM.UpperRegistrationID
            //   // user_Territories = user_Territories
            //};

            Context.Registrations.Add(Reg);
        }

        public List<UserddlVM> GetUsersDLLByHierarchy(int Rank, int RegistrationID = 0)
        {
            var user_dll = (from r in Context.Registrations
                            join u in Context.Users
                            on r.RegistrationID equals u.RegistrationID
                            join h in Context.Organization_hierarchy
                            on u.HierarchyID equals h.HierarchyID

                            where h.Rank < Rank && u.RegistrationID != RegistrationID
                            select new UserddlVM
                            {
                                RegistrationID = u.RegistrationID,
                                Name = r.Name
                                //UserName = r.UserName
                            }).ToList();
            return user_dll;
        }

        public List<UserVM> GetAllUser()
        {
            var users = Context.Users.Include(u => u.Registration).Select(u =>
            new UserVM
            {
                RegistrationID = u.RegistrationID,
                Designation = u.Registration.Designation,
                DOB = u.Registration.DOB,
                HierarchyName = u.Organization_hierarchy.HierarchyName,
                Name = u.Registration.Name,
                NID = u.Registration.NID,
                JoiningDate = u.Registration.JoiningDate,
                OfficeEmail = u.Registration.OfficeEmail,
                PersonalContact = u.Registration.PersonalContact,
                UserName = u.Registration.UserName,
                ReportingTo = u.UpperUser.Registration.Name

            }).ToList();

            return users;
        }

        public List<DDL> GetUserDDL()
        {
            var user = (from u in Context.Users
                        select new DDL
                        {
                            value = u.RegistrationID,
                            label = u.Registration.Name
                        }).ToList();

            return user;
        }

        public List<DDL> GetUserByRankDdl(int rank)
        {
            var user = (from u in Context.Users
                        where u.Organization_hierarchy.Rank == rank
                        select new DDL
                        {
                            value = u.RegistrationID,
                            label = u.Registration.Name
                        }).ToList();

            return user;
        }

        public UserVM GetUserInfo(int id)
        {
            var user = (from u in Context.Users
                        where u.RegistrationID == id
                        select new UserVM
                        {
                            RegistrationID = u.RegistrationID,
                            Designation = u.Registration.Designation,
                            DOB = u.Registration.DOB,
                            HierarchyName = u.Organization_hierarchy.HierarchyName,
                            Rank = u.Organization_hierarchy.Rank,
                            Name = u.Registration.Name,
                            NID = u.Registration.NID,
                            JoiningDate = u.Registration.JoiningDate,
                            OfficeEmail = u.Registration.OfficeEmail,
                            PersonalContact = u.Registration.PersonalContact,
                            UserName = u.Registration.UserName,
                            ReportingTo = u.UpperUser.Registration.Name

                        }).FirstOrDefault();

            return user;
        }

        public UpdateUserVM UpdateDetails(int id)
        {
            var user = Context.Users.Include(u => u.Registration).Include(u => u.UpperUser).Include(u => u.Organization_hierarchy).FirstOrDefault(u => u.RegistrationID == id);


            var model = new UpdateUserVM(user);

            model.UpperUsers = Context.Users.Include(u => u.Registration).Include(u => u.Organization_hierarchy).Where(u => u.Organization_hierarchy.Rank < user.Organization_hierarchy.Rank && u.RegistrationID != id).Select(u => new SelectListItem { Value = u.RegistrationID.ToString(), Text = u.Registration.Name });


            return model;
        }

        public Registration ChangeHierarchyTerritory(UpdateUserVM model)
        {
            var Reg = Context.Registrations.Include(u => u.User).Include(u => u.User.user_Territories).FirstOrDefault(r => r.RegistrationID == model.user.RegistrationID);

            if (Reg == null) return null;

            var HierarchyID = Context.Organization_hierarchy.FirstOrDefault(h => h.Rank == model.Rank).HierarchyID;
            var user_Territories = new List<User_Territory>();

            foreach (var item in model.TerritoryIDs)
            {
                user_Territories.Add(new User_Territory() { TerritoryID = item });
            }

            Reg.User.HierarchyID = HierarchyID;
            Reg.User.Upper_RegistrationID = model.UpperRegistrationID;
            Reg.User.user_Territories = user_Territories;

            //Basic Info update
            Reg.BloodGroup = model.user.Registration.BloodGroup;
            Reg.EmergencyContact = model.user.Registration.EmergencyContact;
            Reg.FatherName = model.user.Registration.FatherName;
            Reg.HomeContact = model.user.Registration.HomeContact;
            Reg.MotherName = model.user.Registration.MotherName;
            Reg.Name = model.user.Registration.Name;
            Reg.OfficeContact = model.user.Registration.OfficeContact;
            Reg.OfficeEmail = model.user.Registration.OfficeEmail;
            Reg.PermanentAddress = model.user.Registration.PermanentAddress;
            Reg.PersonalContact = model.user.Registration.PersonalContact;
            Reg.PersonalEmail = model.user.Registration.PersonalEmail;
            Reg.PresentAddress = model.user.Registration.PresentAddress;

            return Reg;
        }
        public UserDashboardVM GetDashUser(int id)
        {
            var users = Context.Users.Include(u => u.Registration).Select(u =>
            new UserDashboardVM
            {
                RegistrationID = u.RegistrationID,
                Designation = u.Registration.Designation,
                DOB = u.Registration.DOB,
                HierarchyName = u.Organization_hierarchy.HierarchyName,
                Rank = u.Organization_hierarchy.Rank,
                Name = u.Registration.Name,
                NID = u.Registration.NID,
                JoiningDate = u.Registration.JoiningDate,
                OfficeEmail = u.Registration.OfficeEmail,
                PersonalContact = u.Registration.PersonalContact,
                UserName = u.Registration.UserName,
                Photo = u.Registration.Photo,
                Is_default_User = u.IsDefaultUser

            }).FirstOrDefault(u => u.RegistrationID == id);

            return users;
        }
        public UserReportTo GetReportTo(int id)
        {
            var upperID = Find(id).Upper_RegistrationID;
            var ReportUser = Context.Users.Include(u => u.Registration).Select(u =>

            new UserReportTo
            {
                RegistrationID = u.RegistrationID,
                HierarchyName = u.Organization_hierarchy.HierarchyName,
                Rank = u.Organization_hierarchy.Rank,
                Name = u.Registration.Name,
                OfficeEmail = u.Registration.OfficeEmail,
                PersonalContact = u.Registration.PersonalContact
            }).FirstOrDefault(u => u.RegistrationID == upperID);

            if (ReportUser == null)
            {
                ReportUser = new UserReportTo();
            }

            return ReportUser;
        }
        public bool Is_Default_User(int id)
        {
            return Find(id).IsDefaultUser;
        }
        public ICollection<UserReportFrom> GetReportFrom(int id)
        {
            var ReportUserFrom = from u in Context.Users
                                 where u.Upper_RegistrationID == id
                                 select
                                 new UserReportFrom
                                 {
                                     RegistrationID = u.RegistrationID,
                                     HierarchyName = u.Organization_hierarchy.HierarchyName,
                                     Rank = u.Organization_hierarchy.Rank,
                                     Name = u.Registration.Name,
                                     OfficeEmail = u.Registration.OfficeEmail,
                                     PersonalContact = u.Registration.PersonalContact,
                                     UserName = u.Registration.UserName,
                                     AchievedAmount = u.TargetAssigns.Sum(t => (double?)t.AchievedAmount) ?? 0,
                                     TargetAmount = u.TargetAssigns.Sum(t => (double?)t.TargetAmount) ?? 0,
                                     Equipment = u.user_Territories.Count(t => t.Territory.Outlets.Any(o => o.EquipmentDistributions.Any(ed => ed.IsCurrent)))

                                 };

            return ReportUserFrom.ToList();
        }
        public UserTargetReport GetTargetReport(int id)
        {
            var Is_Default_User = Find(id).IsDefaultUser;

            if (Is_Default_User)
            {
                var T_Report = (from t in Context.Targets
                                group t by 0 into gt
                                select new UserTargetReport
                                {
                                    Achieved = gt.Sum(t => (double?)t.Total_AchievedAmount) ?? 0,
                                    Target = gt.Sum(t => (double?)t.Total_TargetAmount) ?? 0
                                }).FirstOrDefault() ?? new UserTargetReport()
                                {
                                    Achieved = 0,
                                    Target = 0
                                };


                var Assigned = (from u in Context.TargetAssigns
                                where u.AssignByRegistrationID == id
                                group u by 0 into g
                                select (g.Sum(t => (double?)t.TargetAmount) ?? 0)).FirstOrDefault();

                T_Report.Assinged = Assigned;
                T_Report.Assignable = T_Report.Target - Assigned;
                T_Report.Assinged_Percentage = T_Report.Target == 0 ? 0 : (T_Report.Assinged * 100) / T_Report.Target;
                T_Report.Achieved_Percentage = T_Report.Target == 0 ? 0 : (T_Report.Achieved * 100) / T_Report.Target;


                return T_Report;
            }
            else
            {
                var T_Report = (from u in Context.Users
                                where u.RegistrationID == id
                                select new UserTargetReport
                                {
                                    Achieved = u.TargetAssigns.Sum(t => (double?)t.AchievedAmount) ?? 0,
                                    Target = u.TargetAssigns.Sum(t => (double?)t.TargetAmount) ?? 0
                                }).FirstOrDefault() ?? new UserTargetReport()
                                {
                                    Achieved = 0,
                                    Target = 0
                                }; ;

                var Assigned = (from u in Context.TargetAssigns
                                where u.AssignByRegistrationID == id
                                group u by 0 into g
                                select (g.Sum(t => (double?)t.TargetAmount) ?? 0)).FirstOrDefault();

                T_Report.Assinged = Assigned;
                T_Report.Assignable = T_Report.Target - Assigned;
                T_Report.Assinged_Percentage = T_Report.Target == 0 ? 0 : (T_Report.Assinged * 100) / T_Report.Target;
                T_Report.Achieved_Percentage = T_Report.Target == 0 ? 0 : (T_Report.Achieved * 100) / T_Report.Target;

                return T_Report;
            }

        }
        public TargetInfo API_TargetInfo(int id)
        {
            var Is_Default_User = Find(id).IsDefaultUser;

            if (Is_Default_User)
            {
                var T_Report = (from t in Context.Targets
                                group t by 0 into gt
                                select new TargetInfo
                                {
                                    Achieved = gt.Sum(t => (double?)t.Total_AchievedAmount) ?? 0,
                                    Target = gt.Sum(t => (double?)t.Total_TargetAmount) ?? 0
                                }).FirstOrDefault() ?? new TargetInfo
                                {
                                    Achieved = 0,
                                    Target = 0
                                };


                var Assigned = (from u in Context.TargetAssigns
                                where u.AssignByRegistrationID == id
                                group u by 0 into g
                                select (g.Sum(t => (double?)t.TargetAmount) ?? 0)).FirstOrDefault();

                T_Report.AchievedPercentage = T_Report.Target == 0 ? 0 : (T_Report.Achieved * 100) / T_Report.Target;


                return T_Report;
            }
            else
            {
                var T_Report = (from u in Context.Users
                                where u.RegistrationID == id
                                select new TargetInfo
                                {
                                    Achieved = u.TargetAssigns.Sum(t => (double?)t.AchievedAmount) ?? 0,
                                    Target = u.TargetAssigns.Sum(t => (double?)t.TargetAmount) ?? 0
                                }).FirstOrDefault() ?? new TargetInfo
                                {
                                    Achieved = 0,
                                    Target = 0
                                };

                var Assigned = (from u in Context.TargetAssigns
                                where u.AssignByRegistrationID == id
                                group u by 0 into g
                                select (g.Sum(t => (double?)t.TargetAmount) ?? 0)).FirstOrDefault();

                T_Report.AchievedPercentage = T_Report.Target == 0 ? 0 : (T_Report.Achieved * 100) / T_Report.Target;

                return T_Report;
            }
        }
        public UserNameRank API_getUserInfo(int id)
        {
            var users = Context.Users.Include(u => u.Registration).Where(u => u.RegistrationID == id).Select(u =>
            new UserNameRank
            {
                Name = u.Registration.Name,
                HierarchyName = u.Organization_hierarchy.HierarchyName,
                Photo = u.Registration.Photo,
                PersonalEmail = u.Registration.PersonalEmail,
                PresentAddress = u.Registration.PresentAddress

            }).FirstOrDefault();

            return users;
        }

        public Registration UserInfoUpdate(int RegID, UserInfoUpdate userInfo)
        {
            var user = Context.Registrations.Find(RegID);

            if (userInfo.Photo != null)
                user.Photo = userInfo.Photo;

            user.PresentAddress = userInfo.PresentAddress;
            user.PersonalEmail = userInfo.PersonalEmail;

            return user;
        }

        public UserDetails GetUserDetails(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return null;

            return Context.Registrations.Where(r => r.UserName == UserName).Select(r => new UserDetails
            {
                Name = r.Name,
                Photo = r.Photo,
                RegistrationID = r.RegistrationID,
                Type = r.Type,
                BloodGroup = r.BloodGroup,
                DOB = r.DOB,
                EmergencyContact = r.EmergencyContact,
                FatherName = r.FatherName,
                HomeContact = r.HomeContact,
                JoiningDate = r.JoiningDate,
                MotherName = r.MotherName,
                NID = r.NID,
                OfficeContact = r.OfficeContact,
                OfficeEmail = r.OfficeEmail,
                PermanentAddress = r.PermanentAddress,
                PersonalContact = r.PersonalContact,
                PersonalEmail = r.PersonalEmail,
                PresentAddress = r.PresentAddress,
                Designation = r.Designation,
                HierarchyName = r.User.Organization_hierarchy.HierarchyName,
                Rank = r.User.Organization_hierarchy.Rank
            }).FirstOrDefault();
        }

        public ICollection<OutletOrderReportModel> OrderReport(UserReportFilterModel filterModel)
        {
            var registrationIds = SubUserIdsByUser(filterModel.RegistrationId);

            var list = (from outletOrderList in Context.OutletOrderLists
                        join outletOrder in Context.OutletOrders on outletOrderList.OutletOrderID equals outletOrder.OutletOrderID
                        join outlet in Context.Outlets on outletOrder.OutletID equals outlet.OutletID
                        join area in Context.Areas on outlet.Territory.AreaID equals area.AreaID
                        where outletOrderList.NetQuantity > 0 &&
                              outletOrder.InsertDate >= filterModel.SDateTime &&
                              outletOrder.InsertDate <= filterModel.EDateTime
                        select new UserReportWithFilterModel
                        {
                            ProductID = outletOrderList.ProductID,
                            ProductName = outletOrderList.Product.ProductName,
                            ProductCode = outletOrderList.Product.ProductCode,
                            OrderQuantity = outletOrderList.OrderQuantity,
                            OrderBy_RegistrationID = outletOrder.OrderBy_RegistrationID,
                            Revenue = outletOrderList.LineTotal,
                            SaleQuantity = outletOrderList.NetQuantity,
                            ApproveBy_RegistrationID = outletOrder.ApproveBy_RegistrationID
                        });

            if (registrationIds != null)
                list = list.Where(o => registrationIds.Contains(o.OrderBy_RegistrationID));

            var group = list.GroupBy(o => new { o.ProductID, o.ProductCode, o.ProductName })
            .Select(g => new OutletOrderReportModel
            {
                ProductID = g.Key.ProductID,
                ProductName = g.Key.ProductName,
                ProductCode = g.Key.ProductCode,
                OrderQuantity = g.Sum(o => o.OrderQuantity)
            }).OrderByDescending(r => r.OrderQuantity).ToList();

            return group;
        }

        public ICollection<OutletOrderReportModel> SalesReport(UserReportFilterModel filterModel)
        {
            var registrationIds = SubUserIdsByUser(filterModel.RegistrationId);

            var list = (from outletOrderList in Context.OutletOrderLists
                        join outletOrder in Context.OutletOrders on outletOrderList.OutletOrderID equals outletOrder.OutletOrderID
                        join outlet in Context.Outlets on outletOrder.OutletID equals outlet.OutletID
                        join area in Context.Areas on outlet.Territory.AreaID equals area.AreaID
                        where outletOrderList.NetQuantity > 0 &&
                              outletOrder.Is_Approved &&
                              outletOrder.InsertDate >= filterModel.SDateTime &&
                              outletOrder.InsertDate <= filterModel.EDateTime
                        select new UserReportWithFilterModel
                        {
                            ProductID = outletOrderList.ProductID,
                            ProductName = outletOrderList.Product.ProductName,
                            ProductCode = outletOrderList.Product.ProductCode,
                            OrderQuantity = outletOrderList.OrderQuantity,
                            OrderBy_RegistrationID = outletOrder.OrderBy_RegistrationID,
                            Revenue = outletOrderList.LineTotal,
                            SaleQuantity = outletOrderList.NetQuantity,
                            ApproveBy_RegistrationID = outletOrder.ApproveBy_RegistrationID
                        });

            if (registrationIds != null)
                list = list.Where(o => registrationIds.Contains(o.ApproveBy_RegistrationID.Value));

            var group = list.GroupBy(o => new { o.ProductID, o.ProductCode, o.ProductName })
            .Select(g => new OutletOrderReportModel
            {
                ProductID = g.Key.ProductID,
                ProductName = g.Key.ProductName,
                ProductCode = g.Key.ProductCode,
                OrderQuantity = g.Sum(o => o.SaleQuantity)
            }).OrderByDescending(r => r.OrderQuantity).ToList();

            return group;
        }

        public ICollection<OutletRevenueReportModelModel> RevenueReport(UserReportFilterModel filterModel)
        {
            var registrationIds = SubUserIdsByUser(filterModel.RegistrationId);

            var list = (from outletOrderList in Context.OutletOrderLists
                        join outletOrder in Context.OutletOrders on outletOrderList.OutletOrderID equals outletOrder.OutletOrderID
                        join outlet in Context.Outlets on outletOrder.OutletID equals outlet.OutletID
                        join area in Context.Areas on outlet.Territory.AreaID equals area.AreaID
                        where outletOrderList.NetQuantity > 0 &&
                              outletOrder.Is_Approved &&
                              outletOrder.InsertDate >= filterModel.SDateTime &&
                              outletOrder.InsertDate <= filterModel.EDateTime
                        select new UserReportWithFilterModel
                        {
                            ProductID = outletOrderList.ProductID,
                            ProductName = outletOrderList.Product.ProductName,
                            ProductCode = outletOrderList.Product.ProductCode,
                            OrderQuantity = outletOrderList.OrderQuantity,
                            OrderBy_RegistrationID = outletOrder.OrderBy_RegistrationID,
                            Revenue = outletOrderList.LineTotal,
                            SaleQuantity = outletOrderList.NetQuantity,
                            ApproveBy_RegistrationID = outletOrder.ApproveBy_RegistrationID
                        });

            if (registrationIds != null)
                list = list.Where(o => registrationIds.Contains(o.ApproveBy_RegistrationID.Value));

            var group = list.GroupBy(o => new { o.ProductID, o.ProductCode, o.ProductName })
            .Select(g => new OutletRevenueReportModelModel
            {
                ProductID = g.Key.ProductID,
                ProductName = g.Key.ProductName,
                ProductCode = g.Key.ProductCode,
                OrderQuantity = g.Sum(o => o.SaleQuantity),
                Revenue = g.Sum(o => o.Revenue)
            }).OrderByDescending(r => r.OrderQuantity).ToList();

            return group;
        }

        public List<UserSR> GetSR_ByDistributorTerritory(int DistributorID)
        {
            var TerritoryIDs = Context.DistributorTerritoryLists
                .Where(d => d.DistributorID == DistributorID)
                .Select(d => d.TerritoryID).ToList();

            var user = (from t in Context.User_Territories
                        join u in Context.Users
                               on t.RegistrationID equals u.RegistrationID
                        where TerritoryIDs.Contains(t.TerritoryID) && u.Organization_hierarchy.HierarchyName == "SR"
                        select new UserSR
                        {
                            RegistrationID = u.RegistrationID,
                            Name = u.Registration.Name,
                            NID = u.Registration.NID,
                            JoiningDate = u.Registration.JoiningDate,
                            OfficeEmail = u.Registration.OfficeEmail,
                            PersonalContact = u.Registration.PersonalContact,
                            UserName = u.Registration.UserName,
                            ReportingTo = u.UpperUser.Registration.Name,
                            AssingedToDistributor = u.Distributor.Name

                        }).Distinct().ToList();

            return user;
        }
        public List<int> SubUserIdsByUser(int registrationId)
        {
            var Ids = new List<int>();
            var cat = Context.Users
                .Include(c => c.SubUsers)
                .Where(c => c.RegistrationID == registrationId)
                .AsEnumerable()?
                .FirstOrDefault();

            if (cat != null)
                UserHierarchyStatic.CatalogIdsFunction(cat.RegistrationID, cat.SubUsers, Ids);

            return Ids;
        }
    }
}