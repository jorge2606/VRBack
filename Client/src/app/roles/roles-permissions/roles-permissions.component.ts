import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { RoleClaimPermission } from './../../_models/role-claim-permission';
import { Roles } from './../../_models/roles';
import { Component, OnInit } from '@angular/core';
import { UpdateRoleClaimPermission } from '../../_models/update-role-claim-permission';
import { TreeviewItem, TreeviewConfig, TreeviewI18n, TreeviewI18nDefault } from 'node_modules/ngx-treeview'
import { RolesPermissionsService } from '../../_services/roles-permissions.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-roles-permissions',
  templateUrl: './roles-permissions.component.html',
  styleUrls: ['./roles-permissions.component.css'],
  providers : [
    {
      provide: TreeviewI18n , useValue: Object.assign(new TreeviewI18nDefault(), 
        {
          getFilterPlaceholder(): string {
            return 'Filtrar Permisos';
          }
        }
      )
    }
  ],
})
export class RolesPermissionsComponent implements OnInit {


  model: Roles;
  id : string;
  name : string;

  constructor(
        private rolesServices: RolesPermissionsService, 
        private route: ActivatedRoute,
        private titleService : Title,
        private toastService : ToastrService) {
          this.titleService.setTitle('Roles - Permisos');
         }

  dropdownEnabled = true;
  items: TreeviewItem[] = [];
  values: number[];
  updateRoleClaimPermission: UpdateRoleClaimPermission = new UpdateRoleClaimPermission()

  config = TreeviewConfig.create({
    hasAllCheckBox: true,
    hasFilter: true,
    hasCollapseExpand: true,
    decoupleChildFromParent: false,
    maxHeight: 400
  });

  buttonClasses = [
    'btn-outline-primary',
    'btn-outline-secondary',
    'btn-outline-success',
    'btn-outline-danger',
    'btn-outline-warning',
    'btn-outline-info',
    'btn-outline-light',
    'btn-outline-dark'
  ];
  buttonClass = this.buttonClasses[0];

  ngOnInit() {

    this.route.params.subscribe(
      p => {
        this.id = p.id;
        this.name = p.name;
      }
    );

    this.rolesServices.getAllRoleClaims(this.id).subscribe(claims => {
      this.items = claims.map(x => new TreeviewItem(x));
    });

  }

  onFilterChange(value: string) {
    console.log('filter:', value);
  }

  mapToRoleClaimPermission(treeViewItem: TreeviewItem) {
    var roleClaim = new RoleClaimPermission();
    roleClaim.checked = treeViewItem.checked;
    roleClaim.text = treeViewItem.text;
    roleClaim.value = treeViewItem.value;
    if (treeViewItem.children)
      roleClaim.children = treeViewItem.children.map(x => this.mapToRoleClaimPermission(x));
    return roleClaim;
  }

  save() {
    
    let childrensUpdate: UpdateRoleClaimPermission;

    this.updateRoleClaimPermission.id = this.id;

    this.updateRoleClaimPermission.Claims = this.items.map(x => this.mapToRoleClaimPermission(x));

    // this.items.forEach(function(value){
    //       value.children.forEach(function(value1){
    //          //childrensUpdate.childrens. = value1.text;
    //          //childrensUpdate.childrens = value1.value;
    //     });
    // });
    this.rolesServices.saveRoleClaims(this.updateRoleClaimPermission)
    .subscribe(
      data => {
        this.toastService.success('Permisos editados correctamente.','',{timeOut : 3000});
      },
      error => {
        console.log("Error", error);
      }
    );
  }
}
