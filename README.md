# xamarin-forms-starter
projeto exemplo em xamarin forms com prism

nesse projeto vou utilizar o Visual Studio 2017

a branch master será uma primeira versão sem alterações, apenas o projeto base

vou separar as funcionalidades por branchs

# requisitos
1. instalar o Visual Studio 2017 (com os pacotes do Xamarin)
2. instalar o template prism (para VS) (https://marketplace.visualstudio.com/items?itemName=BrianLagunas.PrismTemplatePack)
3. colocar a pasta do SKD do Android em uma pasta sem espaços. Por padrão o visual studio instala em c:/arquivos de programas/... e isso pode ocasionar erros de build. Então pegar essa pasta e mover para uma pasta sem espaços (exemplo C:/Andoid). Ir nas configurações do VS, na parte de Xamarin e trocar a pasta que esta apontado o SDK para a pasta que você criou
4. nesse projeto estou utilizando o SKD e API do android 7.1.1 (API 25). Baixe essa API, pois algumas funcionalidades tem que buildar com essa versão
5. ao baixar o projeto, verifique nos pacotes do nuget se precisa atualizar o Xamarin Forms, se sim então atualize
6. recomendo atualizar também no nuget os pacotes da API do android v25.*
