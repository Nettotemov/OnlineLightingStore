/* Обработчик клика на чекбоксах */
$('input[data-check]').on('input', function () {
	let aChecked = [];
	$('input[data-check]:checked').each(function () { aChecked.push($(this).data('check')); });
	localStorage.setItem('CheckboxChecked', aChecked.join(';'));
	localStorage.setItem('PasswordLength', $('#height_opt').val());
});

/* Установка состояний чекбоксов, после загрузки страницы */
// $(document).ready(function () {
// 	if (localStorage.getItem('CheckboxChecked')) {
// 		$('input[data-check]').prop('checked', false);
// 		let aChecked = localStorage.getItem('CheckboxChecked').split(';');
// 		aChecked.forEach(function (str) { $('input[data-check="' + str + '"]').prop('checked', true); });
// 	}
// 	if (localStorage.getItem('PasswordLength')) {
// 		$('#height_opt').val(localStorage.getItem('PasswordLength'));
// 	}
// });

/* Установка состояний чекбоксов, при переходе на сайт по ссылке */
function autoCheck() {
	const urlParams = new URL(window.location.href);
	let checked = urlParams.searchParams.getAll("tags");
	let colorChecked = urlParams.searchParams.getAll("color");
	let typesChecked = urlParams.searchParams.getAll("types");
	if (!checked || !colorChecked || !typesChecked) {
		return;
	}
	console.log(checked);
	console.log(colorChecked);
	console.log(typesChecked);
	for (let i = 0; i < checked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + checked[i] + '"]').checked = true;
		console.log(checked[i]);
	}
	for (let i = 0; i < colorChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + colorChecked[i] + '"]').checked = true;
		console.log(colorChecked[i]);
	}
	for (let i = 0; i < typesChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + typesChecked[i] + '"]').checked = true;
		console.log(typesChecked[i]);
	}
	if (!target) {
		return;
	}
	target.checked = true;
}
autoCheck();

const createURL = () => {
	let urlSearch = new URLSearchParams()
	let types = $("[name=types]:checked").map((_, chk) => chk.value).get()
	let tags = $("[name=tags]:checked").map((_, chk) => chk.value).get()
	let color = $("[name=color]:checked").map((_, chk) => chk.value).get()
	if (types.length > 0) urlSearch.set("types", types.join("&types="))
	if (tags.length > 0) urlSearch.set("tags", tags.join("&tags="))
	if (color.length > 0) urlSearch.set("color", color.join("&color="))
	const srch = '?' + urlSearch.toString();
	console.log(srch);
	window.history.pushState({}, '', srch.replace(/%26/g, "&").replace(/%3D/g, "="));
};
$(function () {
	$("input:checkbox").on("change", createURL)
});

$(document).ready(function () {
	$('.addToCart').on("submit", function (ev) {
		var frm = $(this);
		$.ajax({
			type: 'POST',
			url: '/Cart',
			data: frm.serialize(),
			dataType: "json",
			success: function (str) {
				console.log(str);
				let jsonCart = str;
				$('#quantity').html(jsonCart.quantity);
				$('#sumcart').html(new Intl.NumberFormat('ru-RU', { style: 'currency', currency: 'RUB' }).format(jsonCart.sumCart));
			}
		});
		ev.preventDefault();
		console.log(ev);
	});
});



$('#catalogForm').on('change', 'input[type=checkbox]', function (ev) {
	$('#spinner-border').show();
	let url = document.location.href + "&handler=Products";
	console.log(url);
	var token = GetAntiForgeryToken();
	let prodview = document.getElementById('prod-view');
	console.log(prodview);
	let response = fetch(url)
		.then((response) => response.json())
		.then(data => {
			console.log(data.json)
			let productJson = JSON.parse(data.json)
			let postWrp;
			if (productJson.length == 0) {
				$('#prod-view').empty();
				postWrp = document.createElement("div");
				postWrp.classList.add("col-12");
				postWrp.innerHTML = `<div class="" id="">
					<p class="">Нет товаров</p>
				</div>`
				prodview.append(postWrp);
				$('#spinner-border').hide();
			}
			if (productJson.length > 0) {
				$('#prod-view').empty();
				for (let i = 0; i < productJson.length; i++) {
					postWrp = document.createElement("div");
					postWrp.classList.add("col-3");
					postWrp.innerHTML = `<div class="" id="">
					<div id="img-container">
    					<div class="title-wrp">${productJson[i].Name}</div>
    						<div class="post-and-image">
    							<img class="col-12" src="${productJson[i].MainPhoto}">
    							<div class="description-wrp">${productJson[i].Description}
 							</div>
  						</div>
  						<div class="footer-row">
        					<div class="tags-wrp">${productJson[i].Tags}</div>
							<div class="tags-wrp">${productJson[i].Price}</div>
    					</div>
					</div>
					<a href="/Catalog/${productJson[i].Category.CategoryName}/${productJson[i].Name}/${productJson[i].ProductID}">Подробнее</a>
					<form id="ProductID=${productJson[i].ProductID}" class="addToCart" method="post" action="/Cart" >
						<input type="hidden" name="ProductID" id="pr_ProductID" value="${productJson[i].ProductID}" />
						<input type="hidden" name="returnUrl" value="/Catalog" />
							<input name="${token.name}" type="hidden" value="${token.value}">
					</form>
					<button type="submit" form="ProductID=${productJson[i].ProductID}" class="btn addToCart btn-success btn-sm pull-right" style="float:right">
						Добавить в корзину
					</button>`
					prodview.append(postWrp);
					$('#spinner-border').hide();
				}
			}
			$('.addToCart').on("submit", function (ev) {
				var frm = $(this);
				$.ajax({
					type: 'POST',
					url: '/Cart',
					data: frm.serialize(),
					dataType: "json",
					success: function (str) {
						console.log(str);
						let jsonCart = str;
						$('#quantity').html(jsonCart.quantity);
						$('#sumcart').html(new Intl.NumberFormat('ru-RU', { style: 'currency', currency: 'RUB' }).format(jsonCart.sumCart));
					}
				});
				ev.preventDefault();
				console.log(ev);
			});
		})
});

function GetAntiForgeryToken() {
	var tokenField = $("input[type='hidden'][name$='RequestVerificationToken']");
	if (tokenField.length == 0) {
		return null;
	} else {
		return {
			name: tokenField[0].name,
			value: tokenField[0].value
		};
	}
}

// /* Обработчик клика на чекбоксах */
// $('input').on('input', function () {
// 	let aChecked = [];
// 	$('input[type="checkbox"]:checked').each(function () { aChecked.push($(this).getPath()); });
// 	localStorage.setItem('CheckboxChecked', aChecked.join(';'));
// 	localStorage.setItem('PasswordLength', $('#height_opt').val());
// });

// /* Установка состояний чекбоксов, после загрузки страницы */
// $(document).ready(function () {
// 	if (localStorage.getItem('CheckboxChecked')) {
// 		let aChecked = localStorage.getItem('CheckboxChecked').split(';');
// 		$('input[type="checkbox"]').prop('checked', false);
// 		aChecked.forEach(function (str) { $(str).prop('checked', true); });
// 	}
// 	if (localStorage.getItem('PasswordLength')) {
// 		$('#height_opt').val(localStorage.getItem('PasswordLength'));
// 	}
// });

/************************************************************
 * Функция для jQ, возвращающая уникальный селектор элемента *
 * Источник: https://stackoverflow.com/a/26762730/10179415   *
 ************************************************************/
// jQuery.fn.extend({
// 	getPath: function () {
// 		let pathes = [];
// 		this.each(function (index, element) {
// 			let path, $node = jQuery(element);
// 			while ($node.length) {
// 				let realNode = $node.get(0), name = realNode.localName;
// 				if (!name) { break; }
// 				name = name.toLowerCase();
// 				let parent = $node.parent();
// 				let sameTagSiblings = parent.children(name);
// 				if (sameTagSiblings.length > 1) {
// 					let allSiblings = parent.children();
// 					let index = allSiblings.index(realNode) + 1;
// 					if (index > 0) { name += ':nth-child(' + index + ')'; }
// 				}
// 				path = name + (path ? '>' + path : '');
// 				$node = parent;
// 			}
// 			pathes.push(path);
// 		});
// 		return pathes.join(',');
// 	}
// });
