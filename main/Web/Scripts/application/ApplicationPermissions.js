
function ApplicationPermission(dependencies) {
    
    /******************
    * Private members *
    *******************/

    var self = this,
        applicationPermissionTableId = dependencies.applicationPermissionTableId,
        multiSelectRoleId = dependencies.multiSelectRoleId,
        applicationPermissions,
        rowTemplate = $("#" + applicationPermissionTableId + " thead tr").clone().outerHtml().replace( /th\b/g , "td");

    /******************
    * Private methods *
    *******************/
    var concatApplicationRoles = function(applicationRolesForModule) {

        var applicationCodes = "", applicationDescriptions = "";

        $.each(applicationRolesForModule, function(index, applicationRole) {
            applicationCodes += applicationRole.Code;
            applicationDescriptions += applicationRole.Description;

            if (index < applicationRolesForModule.length - 2) {
                applicationCodes += ", ";
                applicationDescriptions += ", ";
            } else if (index < applicationRolesForModule.length - 1) {
                applicationCodes += " and ";
                applicationDescriptions += " and ";
            }
        });

        return {
            Codes: applicationCodes,
            Descriptions: applicationDescriptions
        };
    },
    isAuthorisedForRolesIn = function(permission) {

        var selectedApplicationIds =
                $("#" + multiSelectRoleId + " :checkbox:checked")
                    .map(function() { return $(this).val(); }),
            applicationIdsForModule =
                permission.ApplicationRoles
                    .map(function(applicationRole) { return applicationRole.Id; });

        return $(selectedApplicationIds).containValues(applicationIdsForModule);
    },
    applyPermissionsForSelectedRoles = function () {

        $.each(applicationPermissions, function (index, permission) {
            
            var isAuthorised = isAuthorisedForRolesIn(permission);
            
            $("#" + applicationPermissionTableId + " tbody tr:eq(" + index + ") :checkbox")
                .prop("checked", isAuthorised)
                .closest("tr")
                .toggleClass("isAuthorised", isAuthorised);
        });
    },
    populateTableWithPermisssions = function (data) {
        applicationPermissions = data;

        $.each(applicationPermissions, function (index, permission) {
            var newRow =
                $(rowTemplate)
                    .find("td[data-property-name]")
                    .each(function () {

                        var cell = $(this);
                        var propertyName = cell.attr("data-property-name"),
                            content = permission[propertyName];

                        if ($.isArray(content)) {
                            role = concatApplicationRoles(content);
                            content = role.Descriptions;
                            if ($(cell).is("[data-property-name=ApplicationRoles]")) {
                                $(cell).attr("title", "Respectives code: " + role.Codes);
                            }
                        } else if (typeof content == "boolean") {

                            content = "<input type='checkbox' disabled";
                            if (isAuthorisedForRolesIn(permission)) {
                                content += " checked";
                                cell.parent().addClass("isAuthorised");
                            }
                            content += "/>";
                        }

                        cell.html(content);
                    })
                    .end();

            $("#" + applicationPermissionTableId + " tbody").append(newRow);

        });
    };

    /******************
    * Public methods  *
    *******************/
    
    //Fires ajax call when called to the specifed url to get all
    //module and the application roles that are authorised to access them

    self.populateApplicationPermissionsFrom = function(allModulePermissionsUrl) {
        $.ajax({
            url: allModulePermissionsUrl,
            type: "POST",
            success: populateTableWithPermisssions
        });

        return self;
    };


    //when any application role is selected it will dynamically apply the permissions for the selected roles
    self.updateApplicationPermissionsWhenRolesSelected = function() {

        $("#" + multiSelectRoleId).find("input:checkbox").click(applyPermissionsForSelectedRoles);
        return self;
    };
    


        
}

