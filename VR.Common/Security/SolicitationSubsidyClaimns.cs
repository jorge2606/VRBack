using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Common.Security
{
    public class SolicitationSubsidyClaims
    {
        public const string CanViewUsers = "user.view";
        public const string CanCreateUsers = "user.create";
        public const string CanEditUsers = "user.edit";
        public const string CanDeleteUsers = "user.delete";
        public const string CanAddSupervisorUsers = "user.addSupervisor";
        public const string CanCreateRelationshipBeetwenSupervisorAndAgent = "user.toCreateRelationshipBeetwenSupervisorAndAgent";
        public const string CanDeleteRelationshipBeetwenSupervisorAndAgent = "user.toDeleteRelationshipBeetwenSupervisorAndAgent";
        public const string CanToSeeRelationshipBeetwenSupervisorAndAgent = "user.toSeeRelationshipBeetwenSupervisorAndAgent";
        public const string CanEditSignatureHolograpichAsAdmin = "user.editSignatureHolograpichAsAdmin";
        public const string CaneditSignatureHolograpichAsSupervisor = "user.editSignatureHolograpichAsSupervisor";

        public const string CanViewCategory = "categories.view";
        public const string CanCreateCategory = "categories.create";
        public const string CanEditCategory = "categories.edit";
        public const string CanDeleteCategory = "categories.delete";

        public const string CanCreateOrganism = "organisms.create";
        public const string CanEditOrganism = "organisms.edit";
        public const string CanDeleteOrganism = "organisms.delete";
        public const string CanViewOrganism = "organisms.view";

        public const string CanCreateDistribution = "distributions.create";
        public const string CanEditDistribution = "distributions.edit";
        public const string CanDeleteDistribution = "distributions.delete";
        public const string CanViewDistribution = "distributions.view";

        public const string CanCreateTransport = "transports.create";
        public const string CanEditTransport = "transports.edit";
        public const string CanDeleteTransport = "transports.delete";
        public const string CanViewTransport = "transports.view";

        public const string CanCreateHoliday = "holidays.create";
        public const string CanEditHoliday = "holidays.edit";
        public const string CanDeleteHoliday = "holidays.delete";
        public const string CanViewHoliday = "holidays.view";

        public const string CanCreateExpenditure = "expenditures.create";
        public const string CanEditExpenditure = "expenditures.edit";
        public const string CanDeleteExpenditure = "expenditures.delete";
        public const string CanViewExpenditure = "expenditures.view";

        public const string CanViewReport = "reports.viewReport";
        public const string CanViewPendingSolicitations = "reports.viewPendingSolicitations";
        public const string CanViewSolicitationsExpire = "reports.viewSolicitationsExpire";
        public const string CanViewExpendituresReport = "reports.viewExpendituresReport";
        public const string CanViewReportByOrganism = "reports.viewReportByOrganism";
        public const string CanViewReportByUsers = "reports.viewReportByUsers";

        public const string CanCreateSolicitation = "solicitations.create";
        public const string CanCreateCommissionSolicitation = "solicitations.createCommission";
        public const string CanUpdateCommissionSolicitation = "solicitations.updateCommission";
        public const string CanCreateRefund = "solicitations.createRefund";
        public const string CanEditSolicitation = "solicitations.edit";
        public const string CanDeleteSolicitation = "solicitations.delete";
        public const string CanViewSolicitation = "solicitations.viewSolicitation";
        public const string CanViewrefund = "solicitations.viewRefund";
        public const string CanModerateSolicitation = "solicitations.moderateSolicitations";
        public const string CanModerateRefund = "solicitations.moderateRefunds";
        public const string CanApproveSolicitation = "solicitations.approveSolicitation";
        public const string CanRejectSolicitation = "solicitations.rejectSolicitation";
        public const string CanApproveRefund = "solicitations.approveRefund";
        public const string CanRejectRefund = "solicitations.rejectRefund";
        public const string CanApproveAccountForSolicitation = "solicitations.approveAccountForSolicitation";
        public const string CanRejectAccountForSolicitation = "solicitations.rejectAccountForSolicitation";
        public const string CanAcceptMySolicitation = "solicitations.acceptMySolicitation";
        public const string canPrintSolicitation = "solicitations.printSolicitation";
        //Imprimir Rendición (finalización súbita)
        public const string canPrintAccountForSolicitation = "solicitations.printAccountForSolicitation";
        //Imprimir Rendición (finalización normal)
        public const string canPrintaccountForNormallyFinalitation = "solicitations.accountForNormallyFinalitation";

        public const string CanCreateRoles = "roles.create";
        public const string CanEditRoles = "roles.edit";
        public const string CanDeleteRoles = "roles.delete";
        public const string CanViewRoles = "roles.view";

        public const string CanCreateLegalRuling = "legalRuling.addLegalRuling";
        public const string CanEditLegalRuling = "legalRuling.editLegalRuling";
        public const string CanDeleteLegalRuling = "legalRuling.deleteLegalRuling";
        public const string CanViewLegalRuling = "legalRuling.viewLegalRuling";

        public const string CanAudits = "audits.view";
    }
}
