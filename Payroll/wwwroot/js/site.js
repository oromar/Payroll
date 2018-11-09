// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function loadDepartments(companySelector, departmentSelector) {
    companySelector = '#' + companySelector;
    departmentSelector = '#' + departmentSelector;
    $.ajax({
        url: '/Departments/DepartmentsByCompany',
        method: 'GET',
        data: { companyId: $(companySelector + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(departmentSelector).empty();
                data.forEach(function (a) {
                    $(departmentSelector).append('<option value=' + a.value + '>' + a.text + '</option>');
                })
            }
        }
    })
}

function loadEmployees(departmentSelector, employeeSelector, callback, isSelect) {
    departmentSelector = '#' + departmentSelector;
    employeeSelector = '#' + employeeSelector;
    $.ajax({
        url: '/Employees/EmployeesByDepartment',
        method: 'GET',
        data: { departmentId: $(departmentSelector + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(employeeSelector).empty();
                data[0] = isSelect ? data[0] : '';
                data.forEach(function (a) {
                    if (a) {
                        if (isSelect) {
                            $(employeeSelector).append('<option value=' + a.value + '>' + a.text + '</option>');
                        } else {
                            $(employeeSelector)
                                .append(
                                    '<div class="checkbox">' +
                                    '<label>' +
                                    '<input type="checkbox" name="EmployeeId" id="EmployeeId_' + a.value + '" value="' + a.value + '"/>' + '<b>' + a.text + '</b>' +
                                    '</label>' +
                                    '</div>');
                        }
                    }
                })
                if (callback) {
                    callback();
                }
            }
        }
    })
}

function loadFunctions(companySelector, functionsSelector) {
    companySelector = "#" + companySelector;
    functionsSelector = "#" + functionsSelector;
    $.ajax({
        url: '/Functions/FunctionsByCompany',
        method: 'GET',
        data: { companyId: $(companySelector + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(functionsSelector).empty();
                data.forEach(function (a) {
                    $(functionsSelector).append('<option value=' + a.value + '>' + a.text + '</option>');
                })
            }
        }
    })
}

function loadJobRoles(companySelector, jobRolesSelector) {
    companySelector = "#" + companySelector;
    jobRolesSelector = "#" + jobRolesSelector;
    $.ajax({
        url: '/JobRoles/JobRolesByCompany',
        method: 'GET',
        data: { companyId: $(companySelector + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(jobRolesSelector).empty();
                data.forEach(function (a) {
                    $(jobRolesSelector).append('<option value=' + a.value + '>' + a.text + '</option>');
                })
            }
        }
    })
}


function loadWorkplaces(companySelector, workplaceSelector) {
    companySelector = "#" + companySelector;
    workplaceSelector = "#" + workplaceSelector;
    $.ajax({
        url: '/Workplaces/WorkplacesByCompany',
        method: 'GET',
        data: { companyId: $(companySelector + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(workplaceSelector).empty();
                data.forEach(function (a) {
                    $(workplaceSelector).append('<option value=' + a.value + '>' + a.text + '</option>');
                })
            }
        }
    })
}

function loadEmployeesByCompany(companySelector, employeeSelector, callback) {
    companySelector = '#' + companySelector;
    employeeSelector = '#' + employeeSelector;
    $.ajax({
        url: '/Employees/EmployeesByCompany',
        method: 'GET',
        data: { companyId: $(companySelector + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(employeeSelector).empty();
                data[0] = '';
                data.forEach(function (a) {
                    if (a) {
                        $(employeeSelector)
                            .append(
                                '<div class="checkbox">' +
                                '<label>' +
                                '<input type="checkbox" name="EmployeeId" id="EmployeeId_' + a.value + '" value="' + a.value + '"/>' + '<b>' + a.text + '</b>' +
                                '</label>' +
                                '</div>');
                    }
                })
                if (callback) {
                    callback();
                }
            }
        }
    })
}

function loadManagersByCompany(companySelector, employeeSelector) {
    companySelector = '#' + companySelector;
    employeeSelector = '#' + employeeSelector;
    $.ajax({
        url: '/Employees/ManagersByCompany',
        method: 'GET',
        data: { companyId: $(companySelector + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(employeeSelector).empty();
                data.forEach(function (a) {
                    if (a) {
                        $(employeeSelector).append('<option value=' + a.value + '>' + a.text + '</option>');
                    }
                })
            }
        }
    })
}

