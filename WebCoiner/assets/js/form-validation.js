class Validator {

	constructor() {}

	// функция, сравнивающая значение поля с переданным регулярным выражением
	comparePattern(formControl, pattern) {
		var input_value = formControl.val();
		var re = new RegExp(pattern);
		return re.test(input_value);
	}

	// функция, проверяющая значение поля на обязательное наличие
	checkRequired(formControl) {
		if (formControl[0].hasAttribute('required')){
			if(formControl.is( "input[type='checkbox']")){
				return formControl.prop('checked');
			}
			else{
				var input_value = formControl.val();
				return !(!input_value || 0 == input_value.length);
			}
		}
		else{
			return true;
		}
	}

	// функция, проверяющая значение поля на совпадение с 
	checkMinMax(formControl, min, max) {
		if (formControl.val()){
			var input_value = parseInt(formControl.val());
			return (input_value >= parseInt(min) && input_value <= parseInt(max));
		}
		else{
			return true;
		}
	}

	// функция помечает formControl как невалидный, создает в родительском .form-group ноду span.validation-tooltip с переданным сообщением
	markInvalid(formControl, validationMessage) {
		var validationTooltips = formControl.parents('.form-group').find('.validation-tooltip');
		if (validationTooltips.length){
			$(validationTooltips[0]).html(validationMessage);
		}
		else{
			formControl.parents('.form-group').append('<span class="validation-tooltip">' + validationMessage + '</span>');
		}
		formControl.removeClass('valid').addClass('invalid');
	}

	// функция помечает formControl как валидный и удаляет ноды span.validation-tooltip
	markValid(formControl) {
		formControl.removeClass('invalid').addClass('valid');
		formControl.parents('.form-group').find('.validation-tooltip').remove();
	}

	// функция убирает все пометки с поля и удаляет ноды span.validation-tooltip
	markNone(formControl) {
		formControl.removeClass('invalid').removeClass('valid');
		formControl.parents('.form-group').find('.validation-tooltip').remove();
	}
	
	// функция проверяет переданный form-control на соответствие required атрибуту
	// в случае ошибок - помечает поле как не валидное (если не установлен флаг "тихо") и возвращает false 
	// в случае успеха - убирает сообщение об ошибке (если не установлен флаг "тихо") и возвращает true 
	validateFieldByDataPattern(formControl, silent){
		if (silent === undefined){
			silent = false;
		}
		if (formControl.val().length && formControl.data('pattern')){
			
			if (this.comparePattern(formControl, formControl.data("pattern"))){
				if (!silent){
					this.markValid(formControl);
				}
				return true;
			}
			else{
				if (!silent){
					this.markInvalid(formControl, 'Check field correctness');
				}
				return false;
			}
		} else{
			if (!silent){
				this.markNone(formControl);
			}
			return true;
		}
	}

	// функция проверяет переданный form-control на соответствие data-pattern
	// в случае ошибок - помечает поле как не валидное (если не установлен флаг "тихо") и возвращает false 
	// в случае успеха - убирает сообщение об ошибке (если не установлен флаг "тихо") и возвращает true 
	validateFieldByRequired(formControl, silent){
		if (silent === undefined){
			silent = false;
		}
		if (!this.checkRequired(formControl)) {
				if ( formControl.is( "input" ) || formControl.is( "textarea" ) ) {
				var validationMessage = 'This field is required';
			}
			if ( formControl.is( "select" ) ) {
				var validationMessage = 'Choose any option';
			}
			if ( formControl.is( ".agreement")) {
				var validationMessage = 'You need to agree with policy';
			}
			if (!silent){
				this.markInvalid(formControl, validationMessage);
			}
			return false;
		}
		else{
			if (!silent){
				this.markValid(formControl);
			}
			return true;
		}
	}

	// функция проверяет переданный form-control на соответствие переданному отрезку мин/макс
	// в случае ошибок - помечает поле как не валидное (если не установлен флаг "тихо") и возвращает false 
	// в случае успеха - убирает сообщение об ошибке (если не установлен флаг "тихо") и возвращает true 
	validateFieldByMinMax(formControl, min, max, silent, currency){
		if (silent === undefined){
			silent = false;
		}
		if (currency === undefined){
			currency = '';
		}
		if (!this.checkMinMax(formControl, min, max)) {
			if (!silent){
				var validation_mesage = 'Сумма не может быть менее '+min;
				if (currency){
					validation_mesage +=  ' ' + currency;
				}
				validation_mesage += ' и более '+max;
				if (currency){
					validation_mesage +=  ' ' + currency;
				}
				this.markInvalid(formControl, validation_mesage);
			}
			return false;
		}
		else{
			if (!silent){
				this.markValid(formControl);
			}
			return true;
		}
	}

	// функция последовательно валидирует все form-control переданный формы
	validateForm(form, silent){
		if (silent === undefined){
			silent = false;
		}
		//собираем поля формы в массив, считаем их количество
		var formControlArray = $(form).find(".form-control:not(:disabled)");
		var formControlCount = formControlArray.length;

		//создаем массив для хранения ошибок валидации
		var errorsArray = Array(formControlCount);

		var self = this;

		//проверяем каждое из полей на валидность
		formControlArray.each(function(index) {
			errorsArray[index] = self.validateFieldByDataPattern($(this), silent) && self.validateFieldByRequired($(this), silent);
		})

		//проверяем массив ошибок на наличе хотя бы одной
		if (errorsArray.some(function(item) { return item == false; })) {
			return false;
		} else {
			return true;
		};
	}
};