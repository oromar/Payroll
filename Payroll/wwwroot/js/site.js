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

function loadEmployees(departmentSelector, employeeSelector) {
    departmentSelector = '#' + departmentSelector;
    employeeSelector = '#' + employeeSelector;
    $.ajax({
        url: '/Employees/EmployeesByDepartment',
        method: 'GET',
        data: { departmentId: $(departmentSelector  + ' option:selected').val() },
        success: function (data) {
            if (data) {
                $(employeeSelector).empty();
                data.forEach(function (a) {
                    $(employeeSelector).append('<option value=' + a.value + '>' + a.text + '</option>');
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
