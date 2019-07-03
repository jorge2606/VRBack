import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClaimsService {

  rolMinistro = "Ministro";
  rolAdmin = "Administrador";
  rolSupervisor = "supervisor";
  rolAgente = "Agente";

  canViewUsers = "user.view";
  canCreateUsers = "user.create";
  canEditUsers = "user.edit";
  canDeleteUsers = "user.delete";
  canAddSupervisorUsers = "user.addSupervisor";
  canCreateRelationshipBeetwenSupervisorAndAgent = "user.toCreateRelationshipBeetwenSupervisorAndAgent";
  canDeleteRelationshipBeetwenSupervisorAndAgent = "user.toDeleteRelationshipBeetwenSupervisorAndAgent";
  canToSeeRelationshipBeetwenSupervisorAndAgent = "user.toSeeRelationshipBeetwenSupervisorAndAgent";
  canEditSignatureHolograpichAsAdmin = "user.editSignatureHolograpichAsAdmin";
  caneditSignatureHolograpichAsSupervisor = "user.editSignatureHolograpichAsSupervisor";

  canViewCategory = "categories.view";
  canCreateCategory = "categories.create";
  canEditCategory = "categories.edit";
  canDeleteCategory = "categories.delete";

  canCreateOrganism = "organisms.create";
  canEditOrganism = "organisms.edit";
  canDeleteOrganism = "organisms.delete";
  canViewOrganism = "organisms.view";

  canCreateDistribution = "distributions.create";
  canEditDistribution = "distributions.edit";
  canDeleteDistribution = "distributions.delete";
  canViewDistribution = "distributions.view";

  canCreateTransport = "transports.create";
  canEditTransport = "transports.edit";
  canDeleteTransport = "transports.delete";
  canViewTransport = "transports.view";

  canCreateHoliday = "holidays.create";
  canEditHoliday = "holidays.edit";
  canDeleteHoliday = "holidays.delete";
  canViewHoliday = "holidays.view";

  canCreateExpenditure = "expenditures.create";
  canEditExpenditure = "expenditures.edit";
  canDeleteExpenditure = "expenditures.delete";
  canViewExpenditure = "expenditures.view";

  canCreateSolicitation = "solicitations.create";
  canCreateCommissionSolicitation = "solicitations.createCommission";
  canCreateRefund = "solicitations.createRefund";
  canEditSolicitation = "solicitations.edit";
  canDeleteSolicitation = "solicitations.delete";
  canViewSolicitation = "solicitations.viewSolicitation";
  canViewrefund = "solicitations.viewRefund";
  canModerateSolicitation = "solicitations.moderateSolicitations";
  canModerateRefund = "solicitations.moderateRefunds";
  canApproveSolicitation = "solicitations.approveSolicitation";
  canRejectSolicitation = "solicitations.rejectSolicitation";
  canApproveRefund = "solicitations.approveRefund";
  canRejectRefund = "solicitations.rejectRefund";
  canApproveAccountForSolicitation = "solicitations.approveAccountForSolicitation";
  canRejectAccountForSolicitation = "solicitations.rejectAccountForSolicitation";
  canAcceptMySolicitation = "solicitations.acceptMySolicitation";

  canCreateRoles = "roles.create";
  canEditRoles = "roles.edit";
  canDeleteRoles = "roles.delete";
  canViewRoles = "roles.view";

  canAudits = "audits.view";

  canViewReport = "reports.viewReport";
  canViewPendingSolicitations = "reports.viewPendingSolicitations";
  canViewSolicitationsExpire = "reports.viewSolicitationsExpire";
  canViewExpendituresReport = "reports.viewExpendituresReport";
  canViewReportByOrganism = "reports.viewReportByOrganism";
  canViewReportByUsers = "reports.viewReportByUsers";

  canCreateLegalRuling = "legalRuling.addLegalRuling";
  canEditLegalRuling = "legalRuling.editLegalRuling";
  canDeleteLegalRuling = "legalRuling.deleteLegalRuling";
  canViewLegalRuling = "legalRuling.viewLegalRuling";

  constructor() { }

  haveClaim(claim: string){
    let current = localStorage.getItem('currentUser');
    if (current){
        return JSON.parse(current)['roles'].find(x => x.value == claim);
    }
  }
}
