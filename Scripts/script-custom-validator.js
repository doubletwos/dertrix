$(document).ready(function () {
	$('#AddOrgForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'OrgName': {
				required: true,
				maxlength: 40,
			},
			'OrgAddress': {
				required: true,
				maxlength: 50,
			},
			'DomainId': {
				required: true,
			},
			'OrgTypeId': {
				required: true,
			},
			'OrgBrandId': {
				required: true,
			},
		},
		messages: {
			'OrgName': {
				required: 'Please enter organisation name',
				maxlength: 'Not more than 40 characters allowed'
			},
			'OrgAddress': {
				required: 'Please enter organisation address',
				maxlength: 'Not more than 50 characters allowed'
			},
			'DomainId': 'Please select organisation sub domain',
			'OrgTypeId': 'Please select organisation type',
			'OrgBrandId': 'Please select organisation brand',
		}
	});
});



	$('#AddOrgType').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'OrgTypeName': {
				required: true,
				maxlength: 20,


			}		
		},
		messages: {
			'OrgTypeName': {
				required: 'Please enter organisation type name',
				maxlength: 'Not more than 20 characters allowed',


			}
		}

	});








$(document).ready(function () {
	$('#AddClass').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'ClassName': {
				required: true,
				maxlength: 20,
			},
			'ClassRefNumb': {
				required: true,
				maxlength: 1,
				digits: true

			},
			'OrgId': {
				required: true,
			}
		},
		messages: {
			'ClassName': {
				required: 'Please enter class name',
				maxlength: 'Not more than 20 characters allowed',
			},
			'ClassRefNumb': {
				required: 'Please enter class reference number',
				maxlength: 'Not more than 1 characters allowed',
				digits: 'Only digits allowed',
			},
			'OrgId': 'Please select organisation'
		}
	});
});






$(document).ready(function () {
	$('#AddStaffForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'FirstName': {
				required: true,
			},
			'LastName': {
				required: true,
			},
			'Email': {
				required: true,
			}
		},
		messages: {
			'FirstName': 'Please enter first name',
			'LastName': 'Please enter last name',
			'Email': 'Please enter email'
		}
	});
});


$(document).ready(function () {
	$('#mySubjectForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'SubjectName': {
				required: true,
				maxlength: 20
			},
			'ClassId': {
				required: true,
			},
			'ClassTeacherId': {
				required: true,
			}
		},
		messages: {
			'SubjectName': {
				required: 'Please enter subject name',
				maxlength: 'Not more than 20 characters allowed'
			},
			'ClassId': 'Please select class',
			'ClassTeacherId': 'Please assign subject to a teacher'
		}
	});

});



$(document).ready(function () {
	$('#AddStudentForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'FirstName': {
				required: true,
			},
			'LastName': {
				required: true,
			},
			'ClassId': {
				required: true,
			},
			'GenderId': {
				required: true,
			},
			'ReligionId': {
				required: true,
			},
			'TribeId': {
				required: true,
			},
			'DateOfBirth': {
				required: true,
			}
		},
		messages: {
			'FirstName': 'Please enter first name',
			'LastName': 'Please enter last name',
			'ClassId': 'Please select class',
			'GenderId': 'Please select gender',
			'ReligionId': 'Please select religion',
			'TribeId': 'Please select tribe',
			'DateOfBirth': 'Please enter Date of birth'
		}
	});




});

$(document).ready(function () {
	$('#myCreatePostForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'PostTopicId': {
				required: true,
			},
			'PostSubject': {
				required: true,
			},
			'PostContent': {
				required: true,
			}
		},
		messages: {
			'PostTopicId': 'Please select appropriate topic',
			'PostSubject': 'Please enter subject',
			'PostContent': 'Post can not be empty'
		}
	});
});



$(document).ready(function () {
	$('#AddNewDertrixUserForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'RegisteredUserTypeId': {
				required: true,
			},
			'IsTester': {
				required: true,
			},
			'FirstName': {
				required: true,
			},
			'LastName': {
				required: true,
			},
			'Email': {
				required: true,
			}
		},
		messages: {
			'RegisteredUserTypeId': 'Please select user type',
			'IsTester': 'Is user a tester?',
			'FirstName': 'Please enter first name',
			'LastName': 'Please enter last name',
			'Email': 'Please enter email address',
		}
	});
});


$(document).ready(function () {
	$('#AddOrgBrandForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'OrgBrandName': {
				required: true,
			},
			'OrgBrandBar': {
				required: true,
			},
			'OrgNavigationBar': {
				required: true,
			},
			'OrgNavBarTextColour': {
				required: true,
			},
			'OrgBrandButtonColour': {
				required: true,
			},
		},
		messages: {
			'OrgBrandName': 'Please enter organisation brand name',
			'OrgBrandBar': 'Please enter organisation brand bar colour code',
			'OrgNavigationBar': 'Please enter organisation nav-bar colour code ',
			'OrgNavBarTextColour': 'Please enter nav-bar text colour code',
			'OrgBrandButtonColour': 'Please enter organisation brand button colour code',
		}
	});
});


$(document).ready(function () {
	$('#AddNewDertrixUserForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'RegisteredUserTypeId': {
				required: true,
			},
			'IsTester': {
				required: true,
			},
			'FirstName': {
				required: true,
			},
			'LastName': {
				required: true,
			},
			'Email': {
				required: true,
			}
		},
		messages: {
			'RegisteredUserTypeId': 'Please select user type',
			'IsTester': 'Is user a tester?',
			'FirstName': 'Please enter first name',
			'LastName': 'Please enter last name',
			'Email': 'Please enter email address',
		}
	});
});


$(document).ready(function () {
	$('#AddDomain').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'DomainName': {
				required: true,
			},
		},
		messages: {
			'DomainName': 'Please enter domain',
		}
	});
});


$(document).ready(function () {
	$('#AddRegisteredUserType').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'RegisteredUserTypeName': {
				required: true,
			},
		},
		messages: {
			'RegisteredUserTypeName': 'Please enter user type name',
		}
	});
});

$(document).ready(function () {
	$('#AddTribe').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'TribeName': {
				required: true,
			},
		},
		messages: {
			'TribeName': 'Please enter tribe name',
		}
	});
});

$(document).ready(function () {
	$('#AddReligion').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'ReligionName': {
				required: true,
			},
		},
		messages: {
			'ReligionName': 'Please enter religion name',
		}
	});
});



$(document).ready(function () {
	$('#AddGender').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'GenderName': {
				required: true,
			},
		},
		messages: {
			'GenderName': 'Please enter gender name',
		}
	});
});


$(document).ready(function () {
	$('#addstudenttypeform').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'Name': {
				required: true,
			},
		},
		messages: {
			'Name': 'Please enter student type name',
		}
	});
});



$(document).ready(function () {
	$('#AddPrischrole').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'RoleName': {
				required: true,
			},
		},
		messages: {
			'RoleName': 'Please enter role name',
		}
	});
});

$(document).ready(function () {
	$('#AddSecSchRole').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'RoleName': {
				required: true,
			},
		},
		messages: {
			'RoleName': 'Please enter role name',
		}
	});
});

$(document).ready(function () {
	$('#AddGroup').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'GroupTypeName': {
				required: true,
			},
		},
		messages: {
			'GroupTypeName': 'Please enter group type name',
		}
	});
});

$(document).ready(function () {
	$('#AddNewPostTopicForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'PostTopicName': {
				required: true,
			},
		},
		messages: {
			'PostTopicName': 'Please enter post topic name',
		}
	});
});


