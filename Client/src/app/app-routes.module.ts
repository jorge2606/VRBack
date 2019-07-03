import { CreateLegalRulingComponent } from './legal-rulings/create-legal-ruling/create-legal-ruling.component';
import { ExpenditureProcedureComponent } from './reports/expenditure-procedure/expenditure-procedure.component';
import { SolicitationsExpireComponent } from './reports/solicitations-expire-procedure/solicitations-expire-procedure.component';
import { ReportSolicitationSubsidyByOrganismComponent } from './reports/report-solicitation-subsidy-by-organism/report-solicitation-subsidy-by-organism.component';
import { NotFoundComponent } from './_helpers/not-found/not-found.component';
import { NotAuthorizedComponent } from './_helpers/not-authorized/not-authorized.component';
import { AuditsComponent } from './audits/audits.component';
import { AccountForNormallyFinalizationComponent } from './solicitation-subsidy/account-for-normally-finalization/account-for-normally-finalization.component';
import { AccountForComponent } from './solicitation-subsidy/account-for/account-for.component';
import { AceptOrRefuseComponent } from './solicitation-subsidy/acept-or-refuse/acept-or-refuse.component';
import { AgentComponent } from './solicitation-subsidy/agent/agent.component';
import { AgentsAndSupervisorsComponent } from './users/agents-and-supervisors/agents-and-supervisors.component';
import { SolicitationSubsidydetailComponent } from './solicitation-subsidy/detail/solicitation-subsidydetail.component';
import { CreateHolidaysComponent } from './holidays/create/create-holidays.component';
import { HolidaysComponent } from './holidays/holidays.component';
import { CreateSolicitationComponent } from './solicitation-subsidy/create/create-solicitation.component';
import { CreateOrganismComponent } from './organisms/create/create-organism.component';
import { CreateExpenditureComponent } from './expenditures/create/create-expenditure.component';
import { ModifyTransportComponent } from './transports/modify/modify-transport.component';
import { CreateTransportComponent } from './transports/create/create-transport.component';
import { CreateDistributionsComponent } from './distributions/create/create-distributions.component';
import { ModifyDistributionComponent } from './distributions/modify/modify-distribution.component';
import { DistributionsComponent } from './distributions/distributions.component';
import { ModifyCategoryComponent } from './category/modify/modify.component';
import { CreateCategoryComponent } from './category/create/create.component';
import { CategoryComponent } from './category/category.component';
import { PhotoProfileComponent } from './users/photo-profile/photo-profile.component';
import { ManagePasswordComponent } from './manage-password/manage-password.component';
import { RolesPermissionsComponent } from './roles/roles-permissions/roles-permissions.component';
import { RolesComponent } from './roles/roles.component';
import { AuthGuard } from './_guards/auth.guard';
import { CreateuserComponent } from './users/create/create.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes} from '@angular/router';
import { LoginComponent } from './login/login.component';
import { UsersComponent } from './users/users.component';
import { HomeComponent } from './home/home.component';
import { ResetPasswordComponent } from './manage-password/reset-password/reset-password.component';
import { SettingofuserComponent } from './users/setting/settingofuser.component';
import { TransportsComponent } from './transports/transports.component';
import { ExpendituresComponent } from './expenditures/expenditures.component';
import { UpdateExpenditureComponent } from './expenditures/update/update-expenditure.component';
import { OrganismsComponent } from './organisms/organisms.component';
import { ModifyOrganismComponent } from './organisms/modify/modify-organism.component';
import { ModifyHolidaysComponent } from './holidays/modify/modify-holidays.component';
import { PrintComponent } from './solicitation-subsidy/print/print.component';
import { SupervisorComponent } from './solicitation-subsidy/supervisor/supervisor.component';
import { RepaymentComponent } from './solicitation-subsidy/repayment/repayment.component';
import { PrintAccountForSolicitationComponent } from './solicitation-subsidy/print-account-for-solicitation/print-account-for-solicitation.component';
import { AuditNotificationComponent } from './audits/audit-notification/audit-notification.component';
import { CanDeactivateGuard } from './directives/can-deactivate-guard';
import { ReportSolicitationSubsidyComponent } from './reports/report-solicitation-subsidy/report-solicitation-subsidy.component';
import { ReportsComponent } from './reports/reports/reports.component';
import { SolicitationsPendingComponent } from './reports/solicitations-pending/solicitations-pending.component';
import { LegalRulingsComponent } from './legal-rulings/legal-rulings.component';
import { AddObservationsComponent } from './modals/add-observations/add-observations.component';
import { ReportCommissionComponent } from './solicitation-subsidy/report-commission/report-commission.component';
import { CreateRolesComponent } from './roles/create/create.component';

const routes: Routes = [
  //canActivate : Interface that a class can implement to be a guard deciding if a route can be activated.
  { path: '', data: {breadcrumb: 'inicio',isHome: true,show: false, },component: HomeComponent, canActivate : [AuthGuard] },
  { path : 'notAuthorized',component : NotAuthorizedComponent, canActivate : [AuthGuard]},
  { path : 'notFound',component : NotFoundComponent, canActivate : [AuthGuard]},
  { path: 'login', component: LoginComponent },

  { path: 'users',
    data: {breadcrumb: 'usuarios',isHome: false,show: true},
    children: [ 
      { path : '',
        component: UsersComponent, canActivate : [AuthGuard]
      },
      { path: 'create',
        component: CreateuserComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard],
        data: {
          breadcrumb: 'crear',
          isHome: false,
          show: true
        } 
      },
      { path: 'AgentsAndSupervisors', 
        data: {breadcrumb: 'agentes y supervisores',isHome: false,show: true},
        component: AgentsAndSupervisorsComponent, canActivate : [AuthGuard] 
      },
      { path: 'update/:id',
        data: {
          breadcrumb: 'modificar',
          isHome: false,
          show: true
        }, component: CreateuserComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard] 
      },
      { path: 'report/:userId',
      data: {
        breadcrumb: 'Reporte de Solicitud por Usuario',
        isHome: false,
        show: true
      }, component: ReportSolicitationSubsidyComponent, canActivate : [AuthGuard]
      }
    ]
  },

  {path : 'administrativeResolution', 
    data : { breadcrumb : 'Resoluciones', ishome : false, show : true }, 
    children :[
    {
      path : '', component : LegalRulingsComponent, canActivate : [AuthGuard]
    },
    {
      path : 'create', component : CreateLegalRulingComponent, canActivate : [AuthGuard]
    },
    {
      path : 'update/:id', component : CreateLegalRulingComponent, canActivate : [AuthGuard]
    }
    ]
  },
  { path: 'settingUser/:id', data: {breadcrumb: 'mi perfil',isHome: false,show: true},
      component: SettingofuserComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard] },
  { path: 'photoProfile/:id', component: PhotoProfileComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard] },

  { path: 'roles',
    data: {breadcrumb: 'roles',isHome: false,show: true}, 
    children : [
      { path : '', component: RolesComponent, canActivate : [AuthGuard] },
      { path : 'create', component : CreateRolesComponent, canActivate : [AuthGuard], data : {breadcrumb : 'crear roles', isHome : false, show : true}},
      { path: 'permissions/:id/:name',data: {breadcrumb: 'roles permisos',isHome: false,show: true}, component: RolesPermissionsComponent, canActivate : [AuthGuard] },  
    ],
  },
  
  { path: 'RecuperarContraseña', component: ManagePasswordComponent},
  { path: 'CambiarPassword', component: ResetPasswordComponent},

  { path: 'category',
    data: {breadcrumb: 'categoría',isHome: false,show: true},  
    children : [
      {path : '',
        component : CategoryComponent, canActivate : [AuthGuard]
      },
      { path: 'create', 
        data: {breadcrumb: 'crear',isHome: false,show: true},
        component : CreateCategoryComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard],},
      { path: 'update/:id', 
        data: {breadcrumb: 'modificar',isHome: false,show: true},
        component : ModifyCategoryComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard],},
    ]
    
  },
  
  { path: 'distribution',
    data: {breadcrumb: 'repartición',isHome: false,show: true},  
    children : [
      {path : '', component : DistributionsComponent, canActivate : [AuthGuard]},
      { path: 'create', component : CreateDistributionsComponent, data: {breadcrumb: 'crear',isHome: false,show: true}, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      { path: 'update/:id', data: {breadcrumb: 'modificar',isHome: false,show: true}, component : ModifyDistributionComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      { path: ':distributionId', data: {breadcrumb: 'usuarios y reparticiones',isHome: false,show: true},component: UsersComponent, canActivate : [AuthGuard] 
      }
    
    ]
  },

  { path: 'transport',
    data: {breadcrumb: 'transporte',isHome: false,show: true},
    children : [
      { path: '', component : TransportsComponent, canActivate : [AuthGuard]},
      { path: 'create', data: {breadcrumb: 'crear',isHome: false,show: true}, component : CreateTransportComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      { path: 'update/:id', data: {breadcrumb: 'modificar',isHome: false,show: true}, component : ModifyTransportComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
    ]
  },
    

  { path: 'expenditure',
    data: {breadcrumb: 'conceptos de gastos',isHome: false,show: true},
    children : [
      { path : '',component : ExpendituresComponent, canActivate : [AuthGuard]},
      { path: 'create', data: {breadcrumb: 'crear', isHome: false,show: true}, component : CreateExpenditureComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      { path: 'update/:id',  data: {breadcrumb: 'modificar', isHome: false,show: true}, component : UpdateExpenditureComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      
    ]
  },

  { path: 'organism',
    data: {breadcrumb: 'Organismos',isHome: false,show: true},
    children : [
      { path : '',component : OrganismsComponent, canActivate : [AuthGuard]},
      { path: 'distributions/:organismId', data: {breadcrumb: 'organismos y reparticiones',isHome: false,show: true}, component : DistributionsComponent, canActivate : [AuthGuard]},
      { path: 'create', data: {breadcrumb: 'crear',isHome: false,show: true}, component : CreateOrganismComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      { path: 'update/:id',data: {breadcrumb: 'modificar',isHome: false,show: true}, component : ModifyOrganismComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      { path: 'report/:id',data: {breadcrumb: 'Reporte de solicitud por organismo',isHome: false,show: true}, component : ReportSolicitationSubsidyByOrganismComponent, canActivate : [AuthGuard]}
    ],
  },

  { path: 'SolicitationSubsidy/agent',
    data: {breadcrumb: 'mis solicitudes de viático',isHome: false,show: true},
    children : [
      { path: '', component : AgentComponent, canActivate : [AuthGuard]},
      { path: 'supervisor', 
        children: [
          { path : 'refunds/:isRefund', data: {breadcrumb: 'reintegro de agentes a mi cargo',isHome: false,show: true}, component : SupervisorComponent, canActivate : [AuthGuard]},
          { path : 'solicitationSubsidies/:isRefund', data: {breadcrumb: 'solicitud de viático de agentes a mi cargo',isHome: false,show: true}, component : SupervisorComponent, canActivate : [AuthGuard]},
        ]},      
      { path: 'create',data: {breadcrumb: 'crear anticipo',isHome: false,show: true}, component : CreateSolicitationComponent, canActivate : [AuthGuard], canDeactivate: [CanDeactivateGuard]},
      { path: 'detail/:id',data: {breadcrumb: 'detalle',isHome: false,show: true}, component : SolicitationSubsidydetailComponent, canActivate : [AuthGuard]},
      { path: 'modify/:id',data: {breadcrumb: 'modificar anticipo',isHome: false,show: true},component : CreateSolicitationComponent, canActivate : [AuthGuard], canDeactivate: [CanDeactivateGuard]},
      { path: 'confirm/:id',data: {breadcrumb: 'confirmar',isHome: false,show: true}, component : AceptOrRefuseComponent, canActivate : [AuthGuard]},
      { path: 'print/:id', data: {breadcrumb: 'vista previa',isHome: false,show: true}, component : PrintComponent, canActivate : [AuthGuard]},
      { path: 'printAccountFor/:id', data: {breadcrumb: 'vista previa',isHome: false,show: true}, component : PrintAccountForSolicitationComponent, canActivate : [AuthGuard]},
      { path: 'repayment',data: {breadcrumb: 'crear reintegro',isHome: false,show: true}, component : RepaymentComponent, canActivate : [AuthGuard],canDeactivate: [CanDeactivateGuard]},
      { path: 'repayment/update/:id',data: {breadcrumb: 'modificar reintegro',isHome: false,show: true}, component : RepaymentComponent, canActivate : [AuthGuard],canDeactivate: [CanDeactivateGuard]},
      { path: 'accountFor/:id',data: {breadcrumb: 'Rendición de gastos',isHome: false,show: true}, component : AccountForComponent, canActivate : [AuthGuard]}
      //{ path: 'printReportCommission/:id',data: {breadcrumb: 'Informe de comisión',isHome: false,show: true}, component : ReportCommissionComponent, canActivate : [AuthGuard]}
      //{ path: 'accountForNormallyFinalitation/:id',data: {breadcrumb: 'Rendición de gastos',isHome: false,show: true}, component : AccountForNormallyFinalizationComponent, canActivate : [AuthGuard]}  
    ],
  },

  { path: 'holidays',  
    data: {breadcrumb: 'feriados',isHome: false,show: true},
    children : [
      { path : '', component : HolidaysComponent, canActivate : [AuthGuard]} ,
      { path: 'create',data: {breadcrumb: 'crear',isHome: false,show: true}, component : CreateHolidaysComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
      { path: 'update/:id' ,data: {breadcrumb: 'modificar',isHome: false,show: true}, component : ModifyHolidaysComponent, canActivate : [AuthGuard], canDeactivate : [CanDeactivateGuard]},
    ]
  },
  
  { path : 'audits',
    data : { breadcrumb : 'Auditoría', isHome : false, show : true },
    children : [
      {path : '', component : AuditsComponent, canActivate : [AuthGuard] },
      {path : 'notifications', component : AuditNotificationComponent, canActivate : [AuthGuard] }
    ]
  },

  { path: 'reports',  
  data: {breadcrumb: 'Reportes',isHome: false,show: true},
  children : [
    { path : 'destinyAndDate', component : ReportsComponent, canActivate : [AuthGuard]} ,
    { path : 'pendingSolicitations', component : SolicitationsPendingComponent, 
      data: {breadcrumb: 'Reportes Pendientes',isHome: false,show: true},
      canActivate : [AuthGuard]
    },
    { path : 'solicitationsExpire', component : SolicitationsExpireComponent, 
      data: {breadcrumb: 'Solicitudes No Rendidas',isHome: false,show: true},
      canActivate : [AuthGuard]
    },
    { path : 'expendituresReport', 
    data: {breadcrumb: 'Conceptos de Gastos mas utilizados',isHome: false,show: true},
    component : ExpenditureProcedureComponent, canActivate : [AuthGuard]}
  ]
  },
  
  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

@NgModule({
  exports : [
    RouterModule
  ], 
  imports : [
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutesModule { }

