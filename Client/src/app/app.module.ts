import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CreateuserComponent } from './users/create/create.component';
import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { UsersComponent } from './users/users.component';
import { AppRoutesModule } from './app-routes.module';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import {NgbModule, NgbDatepickerModule, NgbDateParserFormatter, NgbAlertModule, NgbDatepickerI18n, NgbCollapseModule} from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalContent } from './modals/modals.component';
//Paginator
import {NgxPaginationModule} from 'ngx-pagination';
import { NavarComponent } from './navar/navar.component';
import { RolesComponent } from './roles/roles.component';
import { CreateRolesComponent } from './roles/create/create.component';
import { TreeviewModule } from 'ngx-treeview';
import { RolesPermissionsComponent } from './roles/roles-permissions/roles-permissions.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ManagePasswordComponent } from './manage-password/manage-password.component';
import { ResetPasswordComponent } from './manage-password/reset-password/reset-password.component';
import { SettingofuserComponent } from './users/setting/settingofuser.component';
import { FileUploadModule } from 'ng2-file-upload';
import { PhotoProfileComponent } from './users/photo-profile/photo-profile.component';
import { ListNotificationsComponent } from './modals/list-notifications/list-notifications.component';
import { AuditsComponent } from './audits/audits.component';
import { AuditUsersComponent } from './audits/audit-users/audit-users.component';
import { CategoryComponent } from './category/category.component';
import { CreateCategoryComponent } from './category/create/create.component';
import { ModifyCategoryComponent } from './category/modify/modify.component';
import { DistributionsComponent } from './distributions/distributions.component';
import { ModifyDistributionComponent } from './distributions/modify/modify-distribution.component';
import { CreateDistributionsComponent } from './distributions/create/create-distributions.component';
import { TransportsComponent } from './transports/transports.component';
import { CreateTransportComponent } from './transports/create/create-transport.component';
import { ModifyTransportComponent } from './transports/modify/modify-transport.component';
import { ExpendituresComponent } from './expenditures/expenditures.component';
import { CreateExpenditureComponent } from './expenditures/create/create-expenditure.component';
import { UpdateExpenditureComponent } from './expenditures/update/update-expenditure.component';
import { OrganismsComponent } from './organisms/organisms.component';
import { CreateOrganismComponent } from './organisms/create/create-organism.component';
import { ModifyOrganismComponent } from './organisms/modify/modify-organism.component';
import { CreateSolicitationComponent } from './solicitation-subsidy/create/create-solicitation.component';
import { HolidaysComponent } from './holidays/holidays.component';
import { CreateHolidaysComponent } from './holidays/create/create-holidays.component';
import { ModifyHolidaysComponent } from './holidays/modify/modify-holidays.component';
import { AddNewExpenditureComponent } from './modals/add-new-expenditure/add-new-expenditure.component';
import { AddDestinyComponent } from './modals/add-destiny/add-destiny.component';
import { SolicitationSubsidydetailComponent } from './solicitation-subsidy/detail/solicitation-subsidydetail.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AddSupervisorComponent } from './modals/add-supervisor/add-supervisor.component';
import { AgentsAndSupervisorsComponent } from './users/agents-and-supervisors/agents-and-supervisors.component';
import { NotifyRejectComponent } from './modals/notify-reject/notify-reject.component';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { PrintComponent } from './solicitation-subsidy/print/print.component';
import { SupervisorComponent } from './solicitation-subsidy/supervisor/supervisor.component';
import { AgentComponent } from './solicitation-subsidy/agent/agent.component';
import { AceptOrRefuseComponent } from './solicitation-subsidy/acept-or-refuse/acept-or-refuse.component';
import { HolographSignComponent } from './users/holograph-sign/holograph-sign.component';
import { FileNumberComponent } from './modals/file-number/file-number.component';
import { SelectorDirective } from './directives/selector.directive';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { AlertComponent } from './alert/alert.component';
 
import { ToastrModule } from 'ngx-toastr';
import {NgxMaskModule} from 'ngx-mask';
import { NgxBreadcrumbsModule } from '@nivans/ngx-breadcrumbs';
import { CheckSpaceBlankOnInputDirective } from './directives/check-space-blank-on-input.directive';
import { I18n, CustomLanguageDatepickerI18n } from '@ng-bootstrap/ng-bootstrap/datepicker/CustomLanguagedatepicker-i18n';
import { NgbDateFRParserFormatter } from './holidays/ngb-parseFormatter';
import { RepaymentComponent } from './solicitation-subsidy/repayment/repayment.component';
import { AddExpenditureRepaymentComponent } from './modals/add-expenditure-repayment/add-expenditure-repayment.component';
import { AccountForComponent } from './solicitation-subsidy/account-for/account-for.component';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { DetailAccountForSolicitationComponent } from './solicitation-subsidy/detail-account-for-solicitation/detail-account-for-solicitation.component';
import { PrintAccountForSolicitationComponent } from './solicitation-subsidy/print-account-for-solicitation/print-account-for-solicitation.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { AccountForNormallyFinalizationComponent } from './solicitation-subsidy/account-for-normally-finalization/account-for-normally-finalization.component';
import { AddDestinyRepaymentComponent } from './modals/add-destiny-repayment/add-destiny-repayment.component';
import {CrystalGalleryModule} from 'ngx-crystal-gallery';
import { AuditNotificationComponent } from './audits/audit-notification/audit-notification.component';
import { CanDeactivateGuard } from './directives/can-deactivate-guard';
import { NotFoundComponent } from './_helpers/not-found/not-found.component';
import { NotAuthorizedComponent } from './_helpers/not-authorized/not-authorized.component';
import { ReportSolicitationSubsidyComponent } from './reports/report-solicitation-subsidy/report-solicitation-subsidy.component';
import { ReportSolicitationSubsidyByOrganismComponent } from './reports/report-solicitation-subsidy-by-organism/report-solicitation-subsidy-by-organism.component';
import { ReportsComponent } from './reports/reports/reports.component';
import { SolicitationsPendingComponent } from './reports/solicitations-pending/solicitations-pending.component';
import { SolicitationsExpireComponent } from './reports/solicitations-expire-procedure/solicitations-expire-procedure.component';
import { ExpenditureProcedureComponent } from './reports/expenditure-procedure/expenditure-procedure.component';
import { LegalRulingsComponent } from './legal-rulings/legal-rulings.component';
import { CreateLegalRulingComponent } from './legal-rulings/create-legal-ruling/create-legal-ruling.component';
import { AddObservationsComponent } from './modals/add-observations/add-observations.component';
import { ReportCommissionComponent } from './solicitation-subsidy/report-commission/report-commission.component';

library.add(fas);


@NgModule({
   declarations: [
      AppComponent,
      LoginComponent,
      UsersComponent,
      CreateuserComponent,
      HomeComponent,
      RegisterComponent,
      NgbdModalContent,
      NavarComponent,
      RolesComponent,
      CreateRolesComponent,
      RolesPermissionsComponent,
      ManagePasswordComponent,
      ResetPasswordComponent,
      SettingofuserComponent,
      PhotoProfileComponent,
      ListNotificationsComponent,
      AuditsComponent,
      AuditUsersComponent,
      CategoryComponent,
      CreateCategoryComponent,
      ModifyCategoryComponent,
      DistributionsComponent,
      ModifyDistributionComponent,
      CreateDistributionsComponent,
      TransportsComponent,
      CreateTransportComponent,
      ModifyTransportComponent,
      ExpendituresComponent,
      CreateExpenditureComponent,
      UpdateExpenditureComponent,
      OrganismsComponent,
      CreateOrganismComponent,
      ModifyOrganismComponent,
      CreateSolicitationComponent,
      HolidaysComponent,
      CreateHolidaysComponent,
      ModifyHolidaysComponent,
      AddNewExpenditureComponent,
      AddDestinyComponent,
      SolicitationSubsidydetailComponent,
      AddSupervisorComponent,
      AgentsAndSupervisorsComponent,
      NotifyRejectComponent,
      PrintComponent,
      SupervisorComponent,
      AgentComponent,
      AceptOrRefuseComponent,
      HolographSignComponent,
      FileNumberComponent,
      SelectorDirective,
      AlertComponent,
      CheckSpaceBlankOnInputDirective,
      RepaymentComponent,
      AddExpenditureRepaymentComponent,
      AccountForComponent,
      DetailAccountForSolicitationComponent,
      PrintAccountForSolicitationComponent,
      AccountForNormallyFinalizationComponent,
      AddDestinyRepaymentComponent,
      AuditNotificationComponent,
      NotFoundComponent,
      NotAuthorizedComponent,
      ReportSolicitationSubsidyComponent,
      ReportSolicitationSubsidyByOrganismComponent,
      ReportsComponent,
      SolicitationsPendingComponent,
      SolicitationsExpireComponent,
      ExpenditureProcedureComponent,
      LegalRulingsComponent,
      CreateLegalRulingComponent,
      AddObservationsComponent,
      ReportCommissionComponent
   ],
   imports: [
      BrowserModule,
      AppRoutesModule,
      HttpModule,
      FormsModule,
      HttpClientModule,
      ReactiveFormsModule,
      NgbModule,
      NgxPaginationModule,
      NgbDatepickerModule,
      FontAwesomeModule,
      TreeviewModule.forRoot(),
      FileUploadModule,
      BrowserAnimationsModule,
      NgxSpinnerModule,
      SelectDropDownModule,
      NgbAlertModule,
      ToastrModule.forRoot(), // ToastrModule added
      NgxMaskModule.forRoot(),
      NgxBreadcrumbsModule,
      CollapseModule.forRoot(),
      CrystalGalleryModule
   ],
   providers: [
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter},
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
      Title,
      I18n, {provide: NgbDatepickerI18n, useClass: CustomLanguageDatepickerI18n},
      CurrencyPipe,
      DatePipe,
      CanDeactivateGuard
    ],
   entryComponents: [
      NgbdModalContent,
      ListNotificationsComponent,
      AddNewExpenditureComponent,
      AddDestinyComponent,
      AddSupervisorComponent,
      SolicitationSubsidydetailComponent,
      NotifyRejectComponent,
      FileNumberComponent,
      AddExpenditureRepaymentComponent,
      DetailAccountForSolicitationComponent,
      AddDestinyRepaymentComponent,
      AddObservationsComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
