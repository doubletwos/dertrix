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
			},
			'OrgAddress': {
				required: true,
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
			'OrgName': 'Please enter organisation name',
			'OrgAddress': 'Please enter Organisation address',
			'DomainId': 'Please select organisation domain',
			'OrgTypeId': 'Please select Organisation type',
			'OrgBrandId': 'Please select Organisation brand',
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


	//$('#GuardianForm').validate({
	//	errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
	//	errorElement: 'div',
	//	errorPlacement: function (error, e) {
	//		e.parents('.form-group > div').append(error);
	//	},
	//	highlight: function (e) {
	//		$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
	//		$(e).closest('.help-block').remove();
	//	},
	//	success: function (e) {
	//		e.closest('.form-group').removeClass('has-success has-error');
	//		e.closest('.help-block').remove();
	//	},
	//	rules: {
	//		'firstname1': {
	//			required: true,
	//		},
	//		'LastName': {
	//			required: true,
	//		},
	//		'Email': {
	//			required: true,
	//		}
	//	},
	//	messages: {
	//		'firstname1': 'First name is required',
	//		'LastName': 'Last name is required',
	//		'Email': 'Please enter email address',
	//	}




	//});

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


//$(document).ready(function () {
//	$('#GuardianForm').validate({
//		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
//		errorElement: 'div',
//		errorPlacement: function (error, e) {
//			e.parents('.form-group > div').append(error);
//		},
//		highlight: function (e) {
//			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
//			$(e).closest('.help-block').remove();
//		},
//		success: function (e) {
//			e.closest('.form-group').removeClass('has-success has-error');
//			e.closest('.help-block').remove();
//		},
//		rules: {
//			'FirstName': {
//				required: true,
//			},
//			'LastName': {
//				required: true,
//			},
//			'Email': {
//				required: true,
//			}
//		},
//		messages: {
//			'FirstName': 'First name is required',
//			'LastName': 'Last name is required',
//			'Email': 'Please enter email address',
//		}
//	});
//});





