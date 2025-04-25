function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, ''); // Remove caracteres não numéricos

    // Verifica se o CPF tem 11 caracteres
    if (cpf.length !== 11) {
        return false;
    }

    // Valida os CPFs inválidos, como 00000000000, 11111111111, etc.
    const invalidos = [
        "00000000000", "11111111111", "22222222222", "33333333333",
        "44444444444", "55555555555", "66666666666", "77777777777",
        "88888888888", "99999999999"
    ];

    if (invalidos.includes(cpf)) {
        return false;
    }

    let soma = 0;
    let resto;

    // Validação do primeiro dígito
    for (let i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }
    resto = soma % 11;
    if (resto < 2) {
        resto = 0;
    } else {
        resto = 11 - resto;
    }

    if (cpf.charAt(9) != resto) {
        return false;
    }

    soma = 0;
    // Validação do segundo dígito
    for (let i = 0; i < 10; i++) {
        soma += parseInt(cpf.charAt(i)) * (11 - i);
    }

    resto = soma % 11;
    if (resto < 2) {
        resto = 0;
    } else {
        resto = 11 - resto;
    }

    if (cpf.charAt(10) != resto) {
        return false;
    }

    return true;
}

function telefone_validation(telefone) {
    //retira todos os caracteres menos os numeros
    telefone = telefone.replace(/\D/g, '');

    //verifica se tem a qtde de numero correto
    if (!(telefone.length >= 10 && telefone.length <= 11)) return false;

    //Se tiver 11 caracteres, verificar se começa com 9 o celular
    if (telefone.length == 11 && parseInt(telefone.substring(2, 3)) != 9) return false;

    //verifica se não é nenhum numero digitado errado (propositalmente)
    for (var n = 0; n < 10; n++) {
        //um for de 0 a 9.
        //estou utilizando o metodo Array(q+1).join(n) onde "q" é a quantidade e n é o
        //caractere a ser repetido
        if (telefone == new Array(11).join(n) || telefone == new Array(12).join(n)) return false;
    }
    //DDDs validos
    var codigosDDD = [11, 12, 13, 14, 15, 16, 17, 18, 19,
        21, 22, 24, 27, 28, 31, 32, 33, 34,
        35, 37, 38, 41, 42, 43, 44, 45, 46,
        47, 48, 49, 51, 53, 54, 55, 61, 62,
        64, 63, 65, 66, 67, 68, 69, 71, 73,
        74, 75, 77, 79, 81, 82, 83, 84, 85,
        86, 87, 88, 89, 91, 92, 93, 94, 95,
        96, 97, 98, 99];
    //verifica se o DDD é valido (sim, da pra verificar rsrsrs)
    if (codigosDDD.indexOf(parseInt(telefone.substring(0, 2))) == -1) return false;

    //  E por ultimo verificar se o numero é realmente válido. Até 2016 um celular pode
    //ter 8 caracteres, após isso somente numeros de telefone e radios (ex. Nextel)
    //vão poder ter numeros de 8 digitos (fora o DDD), então esta função ficará inativa
    //até o fim de 2016, e se a ANATEL realmente cumprir o combinado, os numeros serão
    //validados corretamente após esse período.
    //NÃO ADICIONEI A VALIDAÇÂO DE QUAIS ESTADOS TEM NONO DIGITO, PQ DEPOIS DE 2016 ISSO NÃO FARÁ DIFERENÇA
    //Não se preocupe, o código irá ativar e desativar esta opção automaticamente.
    //Caso queira, em 2017, é só tirar o if.
    if (new Date().getFullYear() < 2017) return true;
    if (telefone.length == 10 && [2, 3, 4, 5, 7].indexOf(parseInt(telefone.substring(2, 3))) == -1) return false;

    //se passar por todas as validações acima, então está tudo certo
    return true;
}



function validarFormulario(event) {
    const cpf = document.getElementById('CPF').value; 
    const numero = document.querySelector('#telefone').value;
    const cpfValido = validarCPF(cpf);
    const textoCPF = document.querySelector('#textoCPF');
    const numeroValido = telefone_validation(numero);
    const TelefoneValido = document.querySelector("#TelefoneValido");

    if (cpf == "") {
        texto.CPF = "";
    }

    if (!cpfValido) {
        event.preventDefault(); 
        textoCPF.innerHTML = "Insira um CPF válido";
    }

    if (!numeroValido) {
        event.preventDefault();
        TelefoneValido.innerHTML = "Insira um número válido";
    }
}

document.getElementById('formCadastro').addEventListener('submit', validarFormulario);