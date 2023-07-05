/* Установка состояний чекбоксов, при переходе в конфигуратор по ссылке */
function configuratorAutoCheck() {
	const urlParams = new URL(window.location.href);
	let modelChecked = urlParams.searchParams.getAll("modelName");
	let colorChecked = urlParams.searchParams.getAll("colorProduct");
	let sizeChecked = urlParams.searchParams.getAll("size");
	let lightSourceChecked = urlParams.searchParams.getAll("lightSource");
	let powerWChecked = urlParams.searchParams.getAll("powerW");
	let dimChecked = urlParams.searchParams.getAll("dim");

	const selectModels = document.getElementById("selectModelsProduct");
	const buttonsModels = selectModels.querySelectorAll('button');
	if (modelChecked != null && modelChecked != "") {
		buttonsModels.forEach(button => {
			if (!modelChecked.includes(button.name)) {
				button.disabled = true;
			}
			else {
				button.disabled = false;
				button.classList.add("active");
				$("div.select-model-name").addClass("active");
				$('#configuratorModelName').val(button.name);
			}
		});
	}
	const selectColors = document.getElementById("selectColorsProduct");
	const buttonsColors = selectColors.querySelectorAll('button');
	if (colorChecked != null && colorChecked != "") {
		buttonsColors.forEach(button => {
			if (!colorChecked.includes(button.name)) {
				button.disabled = true;
			}
			else {
				button.disabled = false;
				button.classList.add("active");
				$("div.select-color-product").addClass("active");
				$('#configuratorColorProduct').val(button.name);
			}
		});
	}
	const selectSize = document.getElementById("selectSizeProduct");
	const buttonsSize = selectSize.querySelectorAll('button');
	if (sizeChecked != null && sizeChecked != "") {
		buttonsSize.forEach(button => {
			if (!sizeChecked.includes(button.name)) {
				button.disabled = true;
			}
			else {
				button.disabled = false;
				button.classList.add("active");
				$("div.select-size-product").addClass("active");
				$('#configuratorSizeProduct').val(button.name);
			}
		});
	}
	const selectLightSource = document.getElementById("selectLightSourceProduct");
	const buttonsLightSource = selectLightSource.querySelectorAll('button');
	if (lightSourceChecked != null && lightSourceChecked != "") {
		buttonsLightSource.forEach(button => {
			if (!lightSourceChecked.includes(button.name)) {
				button.disabled = true;
			}
			else {
				button.disabled = false;
				button.classList.add("active");
				$("div.select-light-source-product").addClass("active");
				$('#configuratorLightSourceProduct').val(button.name);
			}
		});
	}
	const selectPowerW = document.getElementById("selectPowerWProduct");
	const buttonsPowerW = selectPowerW.querySelectorAll('button');
	if (powerWChecked != null && powerWChecked != "") {
		buttonsPowerW.forEach(button => {
			if (!powerWChecked.includes(button.name)) {
				button.disabled = true;
			}
			else {
				button.disabled = false;
				button.classList.add("active");
				$("div.select-power-w-product").addClass("active");
				$('#configuratorPowerWProduct').val(button.name);
			}
		});
	}
	const selectDim = document.getElementById("selectDimProduct");
	const buttonsDim = selectDim.querySelectorAll('button');
	if (dimChecked != null && dimChecked != "") {
		buttonsDim.forEach(button => {
			if (!dimChecked.includes(button.name)) {
				button.disabled = true;
			}
			else {
				button.disabled = false;
				button.classList.add("active");
				$("div.select-dim-product").addClass("active");
				$('#configuratorDimProduct').val(button.name);
			}
		});
	}
	try {
		if ((modelChecked[0].length > 0 && buttonsModels.length > 1) || (colorChecked[0].length > 0 && buttonsColors.length > 1) || (sizeChecked[0].length > 0 && buttonsSize.length > 1) || (lightSourceChecked[0].length > 0 && buttonsLightSource.length > 1) || (powerWChecked[0].length > 0 && buttonsPowerW.length > 1) || (dimChecked[0].length > 0 && buttonsDim.length > 1)) {
			$('#clearConfigurator').removeClass("d-none");
			return true;
		} else {
			return false;
		};
	} catch (ex) {
		return false;
	}

}

function configuratorCreateURL() {
	let urlSearch = new URLSearchParams()
	let modelName = $("[name=modelName]:input").map((_, select) => select.value).get()
	let colorProduct = $("[name=colorProduct]:input").map((_, select) => select.value).get()
	let size = $("[name=size]:input").map((_, select) => select.value).get()
	let lightSource = $("[name=lightSource]:input").map((_, select) => select.value).get()
	let powerW = $("[name=powerW]:input").map((_, select) => select.value).get()
	let dim = $("[name=dim]:input").map((_, select) => select.value).get()
	if (modelName.length > 0) urlSearch.set("modelName", modelName.join("&modelName="))
	if (colorProduct.length > 0) urlSearch.set("colorProduct", colorProduct.join("&colorProduct="))
	if (size.length > 0) urlSearch.set("size", size.join("&size="))
	if (lightSource.length > 0) urlSearch.set("lightSource", lightSource.join("&lightSource="))
	if (powerW.length > 0) urlSearch.set("powerW", powerW.join("&powerW="))
	if (dim.length > 0) urlSearch.set("dim", dim.join("&dim="))
	const srch = '?' + urlSearch.toString();
	window.history.pushState({}, '', srch.replace(/%26/g, "&").replace(/%3D/g, "="));
};


$(document).ready(function () {
	let isCheckedModel = false;
	let isCheckedColor = false;
	let isCheckedSize = false;
	let isCheckedSourceLight = false;
	let isCheckedPowerW = false;
	let isCheckedDim = false;

	$('div.select-model-name button.radio-btn').on('click', function () {
		var inputValue = $('#configuratorModelName').val($(this).data('value'));
		$("div.select-model-name .radio-btn.active").removeClass("active");
		$(this).addClass("active");
		$(this).find("::before").css("background-color", "#ccc");
		$("div.select-model-name").addClass("active");

		configuratorCreateURL(); //меняем url
		let path = document.location.href; // забираем путь
		console.log(path);
		var xhr = new XMLHttpRequest();
		xhr.open("GET", path + "&handler=Params", true);
		xhr.onreadystatechange = function () {
			if (xhr.readyState == 4 && xhr.status == 200) {
				console.log(xhr.status);
				var result = JSON.parse(xhr.responseText);
				console.log(result);
				// document.getElementById("result").innerHTML = result.result;

				let colorsJson = JSON.parse(result.colorsJson);
				console.log(colorsJson);
				const selectColors = document.getElementById("selectColorsProduct");
				const buttons = selectColors.querySelectorAll('button');
				buttons.forEach(button => {
					if (!colorsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let sizesJson = JSON.parse(result.sizesJson);
				console.log(sizesJson);
				const selectSizes = document.getElementById("selectSizeProduct");
				const buttonsSizes = selectSizes.querySelectorAll('button');
				buttonsSizes.forEach(button => {
					if (!sizesJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let lightSourceJson = JSON.parse(result.lightSourceJson);
				console.log(lightSourceJson);
				const selectLightSource = document.getElementById("selectLightSourceProduct");
				const buttonsLightSource = selectLightSource.querySelectorAll('button');
				buttonsLightSource.forEach(button => {
					if (!lightSourceJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let powerWsJson = JSON.parse(result.powerWsJson);
				console.log(powerWsJson);
				const selectPowerWs = document.getElementById("selectPowerWProduct");
				const buttonsPowerWs = selectPowerWs.querySelectorAll('button');
				buttonsPowerWs.forEach(button => {
					if (!powerWsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let addControlsJson = JSON.parse(result.addControlsJson);
				console.log(addControlsJson);
				const selectDim = document.getElementById("selectDimProduct");
				const buttonsDim = selectDim.querySelectorAll('button');
				buttonsDim.forEach(button => {
					if (!addControlsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});
			}
		};
		xhr.send(JSON.stringify({ inputValue: inputValue }));
		isCheckedModel = true;
	});

	$('div.selectColorsProduct .radio-btn').on('click', function () {
		var inputValue = $('#configuratorColorProduct').val($(this).data('value'));
		$("div.selectColorsProduct .radio-btn.active").removeClass("active");
		$(this).addClass("active");
		$(this).find("::before").css("background-color", "#ccc");
		$("div.select-color-product").addClass("active");

		configuratorCreateURL(); //меняем url
		let path = document.location.href; // забираем путь
		console.log(path);
		var xhr = new XMLHttpRequest();
		xhr.open("GET", path + "&handler=Params", true);
		xhr.onreadystatechange = function () {
			if (xhr.readyState == 4 && xhr.status == 200) {
				console.log(xhr.status);
				var result = JSON.parse(xhr.responseText);
				// console.log(result);

				let modelsJson = JSON.parse(result.modelsJson);
				console.log(modelsJson);
				const selectModels = document.getElementById("selectModelsProduct");
				const buttonsModels = selectModels.querySelectorAll('button');
				buttonsModels.forEach(button => {
					if (!modelsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let sizesJson = JSON.parse(result.sizesJson);
				console.log(sizesJson);
				const selectSizes = document.getElementById("selectSizeProduct");
				const buttonsSizes = selectSizes.querySelectorAll('button');
				buttonsSizes.forEach(button => {
					if (!sizesJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let lightSourceJson = JSON.parse(result.lightSourceJson);
				console.log(lightSourceJson);
				const selectLightSource = document.getElementById("selectLightSourceProduct");
				const buttonsLightSource = selectLightSource.querySelectorAll('button');
				buttonsLightSource.forEach(button => {
					if (!lightSourceJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let powerWsJson = JSON.parse(result.powerWsJson);
				console.log(modelsJson);
				const selectPowerWs = document.getElementById("selectPowerWProduct");
				const buttonsPowerWs = selectPowerWs.querySelectorAll('button');
				buttonsPowerWs.forEach(button => {
					if (!powerWsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let addControlsJson = JSON.parse(result.addControlsJson);
				console.log(addControlsJson);
				const selectDim = document.getElementById("selectDimProduct");
				const buttonsDim = selectDim.querySelectorAll('button');
				buttonsDim.forEach(button => {
					if (!addControlsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});
			}
		};
		xhr.send(JSON.stringify({ inputValue: inputValue }));
		isCheckedColor = true;
	});

	$('div.selectSizeProduct .radio-btn').on('click', function () {
		var inputValue = $('#configuratorSizeProduct').val($(this).data('value'));
		$("div.selectSizeProduct .radio-btn.active").removeClass("active");
		$(this).addClass("active");
		$(this).find("::before").css("background-color", "#ccc");
		$("div.select-size-product").addClass("active");

		configuratorCreateURL(); //меняем url
		let path = document.location.href; // забираем путь
		console.log(path);
		var xhr = new XMLHttpRequest();
		xhr.open("GET", path + "&handler=Params", true);
		xhr.onreadystatechange = function () {
			if (xhr.readyState == 4 && xhr.status == 200) {
				console.log(xhr.status);
				var result = JSON.parse(xhr.responseText);
				// console.log(result);

				let modelsJson = JSON.parse(result.modelsJson);
				console.log(modelsJson);
				const selectModels = document.getElementById("selectModelsProduct");
				const buttonsModels = selectModels.querySelectorAll('button');
				buttonsModels.forEach(button => {
					if (!modelsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let colorsJson = JSON.parse(result.colorsJson);
				console.log(colorsJson);
				const selectColors = document.getElementById("selectColorsProduct");
				const buttons = selectColors.querySelectorAll('button');
				buttons.forEach(button => {
					if (!colorsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let lightSourceJson = JSON.parse(result.lightSourceJson);
				console.log(lightSourceJson);
				const selectLightSource = document.getElementById("selectLightSourceProduct");
				const buttonsLightSource = selectLightSource.querySelectorAll('button');
				buttonsLightSource.forEach(button => {
					if (!lightSourceJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let powerWsJson = JSON.parse(result.powerWsJson);
				console.log(modelsJson);
				const selectPowerWs = document.getElementById("selectPowerWProduct");
				const buttonsPowerWs = selectPowerWs.querySelectorAll('button');
				buttonsPowerWs.forEach(button => {
					if (!powerWsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let addControlsJson = JSON.parse(result.addControlsJson);
				console.log(addControlsJson);
				const selectDim = document.getElementById("selectDimProduct");
				const buttonsDim = selectDim.querySelectorAll('button');
				buttonsDim.forEach(button => {
					if (!addControlsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});
			}
		};
		xhr.send(JSON.stringify({ inputValue: inputValue }));
		isCheckedSize = true;
	});

	$('div.selectLightSourceProduct .radio-btn').on('click', function () {
		var inputValue = $('#configuratorLightSourceProduct').val($(this).data('value'));
		$("div.selectLightSourceProduct .radio-btn.active").removeClass("active");
		$(this).addClass("active");
		$(this).find("::before").css("background-color", "#ccc");
		$("div.select-light-source-product").addClass("active");

		configuratorCreateURL(); //меняем url
		let path = document.location.href; // забираем путь
		console.log(path);
		var xhr = new XMLHttpRequest();
		xhr.open("GET", path + "&handler=Params", true);
		xhr.onreadystatechange = function () {
			if (xhr.readyState == 4 && xhr.status == 200) {
				console.log(xhr.status);
				var result = JSON.parse(xhr.responseText);
				// console.log(result);


				let modelsJson = JSON.parse(result.modelsJson);
				console.log(modelsJson);
				const selectModels = document.getElementById("selectModelsProduct");
				const buttonsModels = selectModels.querySelectorAll('button');
				buttonsModels.forEach(button => {
					if (!modelsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let colorsJson = JSON.parse(result.colorsJson);
				console.log(colorsJson);
				const selectColors = document.getElementById("selectColorsProduct");
				const buttons = selectColors.querySelectorAll('button');
				buttons.forEach(button => {
					if (!colorsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let sizesJson = JSON.parse(result.sizesJson);
				console.log(sizesJson);
				const selectSizes = document.getElementById("selectSizeProduct");
				const buttonsSizes = selectSizes.querySelectorAll('button');
				buttonsSizes.forEach(button => {
					if (!sizesJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let powerWsJson = JSON.parse(result.powerWsJson);
				console.log(modelsJson);
				const selectPowerWs = document.getElementById("selectPowerWProduct");
				const buttonsPowerWs = selectPowerWs.querySelectorAll('button');
				buttonsPowerWs.forEach(button => {
					if (!powerWsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let addControlsJson = JSON.parse(result.addControlsJson);
				console.log(addControlsJson);
				const selectDim = document.getElementById("selectDimProduct");
				const buttonsDim = selectDim.querySelectorAll('button');
				buttonsDim.forEach(button => {
					if (!addControlsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});
			}
		};
		xhr.send(JSON.stringify({ inputValue: inputValue }));
		isCheckedSourceLight = true;
	});


	$('div.selectPowerWProduct .radio-btn').on('click', function () {
		var inputValue = $('#configuratorPowerWProduct').val($(this).data('value'));
		$("div.selectPowerWProduct .radio-btn.active").removeClass("active");
		$(this).addClass("active");
		$(this).find("::before").css("background-color", "#ccc");
		$("div.select-power-w-product").addClass("active");

		configuratorCreateURL(); //меняем url
		let path = document.location.href; // забираем путь
		console.log(path);
		var xhr = new XMLHttpRequest();
		xhr.open("GET", path + "&handler=Params", true);
		xhr.onreadystatechange = function () {
			if (xhr.readyState == 4 && xhr.status == 200) {
				console.log(xhr.status);
				var result = JSON.parse(xhr.responseText);
				// console.log(result);


				let modelsJson = JSON.parse(result.modelsJson);
				console.log(modelsJson);
				const selectModels = document.getElementById("selectModelsProduct");
				const buttonsModels = selectModels.querySelectorAll('button');
				buttonsModels.forEach(button => {
					if (!modelsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let colorsJson = JSON.parse(result.colorsJson);
				console.log(colorsJson);
				const selectColors = document.getElementById("selectColorsProduct");
				const buttons = selectColors.querySelectorAll('button');
				buttons.forEach(button => {
					if (!colorsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let sizesJson = JSON.parse(result.sizesJson);
				console.log(sizesJson);
				const selectSizes = document.getElementById("selectSizeProduct");
				const buttonsSizes = selectSizes.querySelectorAll('button');
				buttonsSizes.forEach(button => {
					if (!sizesJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let lightSourceJson = JSON.parse(result.lightSourceJson);
				console.log(lightSourceJson);
				const selectLightSource = document.getElementById("selectLightSourceProduct");
				const buttonsLightSource = selectLightSource.querySelectorAll('button');
				buttonsLightSource.forEach(button => {
					if (!lightSourceJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let addControlsJson = JSON.parse(result.addControlsJson);
				console.log(addControlsJson);
				const selectDim = document.getElementById("selectDimProduct");
				const buttonsDim = selectDim.querySelectorAll('button');
				buttonsDim.forEach(button => {
					if (!addControlsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});
			}
		};
		xhr.send(JSON.stringify({ inputValue: inputValue }));
		isCheckedPowerW = true;
	});


	$('div.selectDimProduct .radio-btn').on('click', function () {
		var inputValue = $('#configuratorDimProduct').val($(this).data('value'));
		$("div.selectDimProduct .radio-btn.active").removeClass("active");
		$(this).addClass("active");
		$(this).find("::before").css("background-color", "#ccc");
		$("div.select-dim-product").addClass("active");

		configuratorCreateURL(); //меняем url
		let path = document.location.href; // забираем путь
		console.log(path);
		var xhr = new XMLHttpRequest();
		xhr.open("GET", path + "&handler=Params", true);
		xhr.onreadystatechange = function () {
			if (xhr.readyState == 4 && xhr.status == 200) {
				console.log(xhr.status);
				var result = JSON.parse(xhr.responseText);
				// console.log(result);


				let modelsJson = JSON.parse(result.modelsJson);
				console.log(modelsJson);
				const selectModels = document.getElementById("selectModelsProduct");
				const buttonsModels = selectModels.querySelectorAll('button');
				buttonsModels.forEach(button => {
					if (!modelsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let colorsJson = JSON.parse(result.colorsJson);
				console.log(colorsJson);
				const selectColors = document.getElementById("selectColorsProduct");
				const buttons = selectColors.querySelectorAll('button');
				buttons.forEach(button => {
					if (!colorsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let sizesJson = JSON.parse(result.sizesJson);
				console.log(sizesJson);
				const selectSizes = document.getElementById("selectSizeProduct");
				const buttonsSizes = selectSizes.querySelectorAll('button');
				buttonsSizes.forEach(button => {
					if (!sizesJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let lightSourceJson = JSON.parse(result.lightSourceJson);
				console.log(lightSourceJson);
				const selectLightSource = document.getElementById("selectLightSourceProduct");
				const buttonsLightSource = selectLightSource.querySelectorAll('button');
				buttonsLightSource.forEach(button => {
					if (!lightSourceJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});


				let powerWsJson = JSON.parse(result.powerWsJson);
				console.log(powerWsJson);
				const selectPowerWs = document.getElementById("selectPowerWProduct");
				const buttonsPowerWs = selectPowerWs.querySelectorAll('button');
				buttonsPowerWs.forEach(button => {
					if (!powerWsJson.includes(button.name)) {
						button.disabled = true;
					}
					else {
						button.disabled = false;
					}
				});
			}
		};
		xhr.send(JSON.stringify({ inputValue: inputValue }));
		isCheckedDim = true;
	});

	const captionProduct = document.getElementById('configuratorSubproductCaption');
	const priceProduct = document.getElementById('configuratorProductPrice');
	const btnArrow3 = document.getElementById('btn-arrow-3');
	const idProduct = document.getElementById('configuratorProductId');
	const addToCart = document.getElementById('configuratorAddToCart');
	const imgProduct = document.getElementById('configuratorSubproductImg');
	let displayBtnClear = configuratorAutoCheck();

	$('#clearConfigurator').on('click', async function () {
		if (displayBtnClear === true) {
			$('#clearConfigurator').addClass("d-none");
			displayBtnClear = false;
		}

		const selectDimProduct = document.getElementById('selectDimProduct');
		const dimButtons = selectDimProduct.querySelectorAll('button');
		if (dimButtons.length > 1) {
			dimButtons.forEach(button => {
				button.disabled = false;
				isCheckedDim = false;
				$('#configuratorDimProduct').val("");
			});
			$("#selectDimProduct .radio-btn.active").removeClass("active");
			$(".select-dim-product").removeClass("active");
		}

		const selectPowerWProduct = document.getElementById('selectPowerWProduct');
		const powerWButtons = selectPowerWProduct.querySelectorAll('button');
		if (powerWButtons.length > 1) {
			powerWButtons.forEach(button => {
				button.disabled = false;
				isCheckedPowerW = false;
				$('#configuratorPowerWProduct').val("");
			});
			$("#selectPowerWProduct .radio-btn.active").removeClass("active");
			$(".select-power-w-product").removeClass("active");
		}

		const selectLightSourceProduct = document.getElementById('selectLightSourceProduct');
		const lightSourceButtons = selectLightSourceProduct.querySelectorAll('button');
		if (lightSourceButtons.length > 1) {
			lightSourceButtons.forEach(button => {
				button.disabled = false;
				isCheckedSourceLight = false;
				$('#configuratorLightSourceProduct').val("");
			});
			$("#selectLightSourceProduct .radio-btn.active").removeClass("active");
			$(".select-light-source-product").removeClass("active");
		}

		const selectSizeProduct = document.getElementById('selectSizeProduct');
		const sizeButtons = selectSizeProduct.querySelectorAll('button');
		if (sizeButtons.length > 1) {
			sizeButtons.forEach(button => {
				button.disabled = false;
				isCheckedSize = false;
				$('#configuratorSizeProduct').val("");
			});
			$("#selectSizeProduct .radio-btn.active").removeClass("active");
			$(".select-size-product").removeClass("active");
		}

		const selectColorsProduct = document.getElementById('selectColorsProduct');
		const colorsButtons = selectColorsProduct.querySelectorAll('button');
		if (colorsButtons.length > 1) {
			colorsButtons.forEach(button => {
				button.disabled = false;
				isCheckedColor = false;
				$('#configuratorColorProduct').val("");
			});
			$("#selectColorsProduct .radio-btn.active").removeClass("active");
			$(".select-color-product").removeClass("active");
		}

		const selectModelsProduct = document.getElementById('selectModelsProduct');
		const modelsButtons = selectModelsProduct.querySelectorAll('button');
		if (modelsButtons.length > 1) {
			modelsButtons.forEach(button => {
				button.disabled = false;
				isCheckedModel = false;
				$('#configuratorModelName').val("");
			});
			$("#selectModelsProduct .radio-btn.active").removeClass("active");
			$(".select-model-name").removeClass("active");
		}

		configuratorCreateURL();
		$("div.configurator__wrapper").removeClass("checked");

		let path = document.location.href + "&handler=Params"; // забираем путь
		let response = await fetch(path)
		let commits = await response.json();
		let collectionNameJson = JSON.parse(commits.collectionNameJson);
		console.log(collectionNameJson);

		// try {
		// 	collectionNameJson = JSON.parse(commits.collectionNameJson);
		// } catch (ex) {
		// 	collectionNameJson = JSON.parse(commits.modelsJson);
		// }

		let minPriceJson = JSON.parse(commits.minPriceJson);
		let defaultImgJson = JSON.parse(commits.defaultImgJson);

		captionProduct.classList.add("text-muted");
		captionProduct.innerText = collectionNameJson[0].toUpperCase() + collectionNameJson.substr(1);

		priceProduct.classList.add("text-muted");
		priceProduct.innerText = "Цена: " + minPriceJson.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB', maximumFractionDigits: 0 });

		btnArrow3.classList.add("disabled");

		idProduct.value = "";
		addToCart.disabled = true;
		addToCart.classList.replace("addToCart__product-card", "addToCart__product-card-disabled");

		imgProduct.src = defaultImgJson;
	});

	$('#collectionPageForm').on('click', ".radio-btn", async function () {
		let path = document.location.href + "&handler=Params"; // забираем путь
		let response = await fetch(path)
		let commits = await response.json();

		let configuratorDimProduct = $('#configuratorDimProduct').val()
		if (configuratorDimProduct !== undefined && configuratorDimProduct !== "") {
			isCheckedDim = true;
		}

		let configuratorPowerWProduct = $('#configuratorPowerWProduct').val();
		if (configuratorPowerWProduct !== undefined && configuratorPowerWProduct !== "") {
			isCheckedPowerW = true;
		}

		let configuratorLightSourceProduct = $('#configuratorLightSourceProduct').val();
		if (configuratorLightSourceProduct !== undefined && configuratorLightSourceProduct !== "") {
			isCheckedSourceLight = true;
		}

		let configuratorSizeProduct = $('#configuratorSizeProduct').val();
		if (configuratorSizeProduct !== undefined && configuratorSizeProduct !== "") {
			isCheckedSize = true;
		}

		let configuratorColorProduct = $('#configuratorColorProduct').val();
		if (configuratorColorProduct !== undefined && configuratorColorProduct !== "") {
			isCheckedColor = true;
		}

		let configuratorModelName = $('#configuratorModelName').val();
		let modelsJson = JSON.parse(commits.modelsJson);
		if (configuratorModelName !== undefined && configuratorModelName !== "" || modelsJson == "-") {
			isCheckedModel = true;
		}

		if (displayBtnClear === false) {
			$('#clearConfigurator').removeClass("d-none");
			displayBtnClear = true;
		}

		if (isCheckedModel == true && isCheckedColor == true && isCheckedSize == true && isCheckedSourceLight == true && isCheckedPowerW == true && isCheckedDim == true) {
			$("div.configurator__wrapper").addClass("checked");
			let productJson = JSON.parse(commits.productJson);
			
			if (JSON.stringify(productJson) !== '{}') {
				try {
					captionProduct.classList.remove("text-muted");
					captionProduct.innerText = productJson.Name;
				} catch (error) {
					console.log("Не удалось удалить элемент: " + error);
					captionProduct.innerText = productJson.Name;
				}


				priceProduct.classList.remove("text-muted");
				priceProduct.innerText = "Цена: " + productJson.Price.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB', maximumFractionDigits: 0 });

				btnArrow3.setAttribute("href", "/catalog/" + productJson.MetaData.Url + "/" + productJson.Id);
				btnArrow3.classList.remove("disabled");

				idProduct.value = productJson.Id;
				addToCart.disabled = false;
				addToCart.classList.replace("addToCart__product-card-disabled", "addToCart__product-card");

				imgProduct.src = productJson.MainPhoto;
			}

		}
	});
});