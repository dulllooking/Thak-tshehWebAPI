(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2d0aba5b"],{"15bc":function(e,t,s){"use strict";s.r(t);var i=s("7a23"),r={class:"pt-10"},c={class:"bg-secondary py-10"},a={class:"container"},o=Object(i["i"])("h2",{class:"text-dark text-center mb-5 display-6 fw-bold"},"- 註冊 -",-1),l={class:"row justify-content-center"},n={class:"col-md-6"},d={class:"border border-dark border-widths-2 rounded-6 px-6 py-7"},b={class:"mb-4"},u={class:"d-flex justify-content-between"},p=Object(i["i"])("label",{for:"emailAddress",class:"form-label fs-4 text-dark"},"電子信箱",-1),f=Object(i["i"])("span",{class:"material-icons fs-6 me-1"},"error",-1),m={class:"fs-8 mb-0"},h={class:"input-group"},j={class:"mb-4"},O=Object(i["i"])("label",{for:"signUpPassword",class:"form-label fs-4 text-dark"},"密碼",-1),g={class:"input-group"},k=Object(i["i"])("div",{class:"errorMessenger text-dark d-flex mt-2"},[Object(i["i"])("span",{class:"material-icons fs-6 me-1"}," error "),Object(i["i"])("p",{class:"fs-8"},"密碼長度必須大於8個字元")],-1),w={class:"mb-5"},v=Object(i["i"])("label",{for:"signUpConfirmPassword",class:"form-label fs-4 text-dark"},"確認密碼",-1),x=Object(i["i"])("div",{class:"input-group"},[Object(i["i"])("input",{type:"password",class:"form-control-lightYellow border-0 rounded-pill position-relative py-13",id:"signUpConfirmPassword",placeholder:""}),Object(i["i"])("span",{class:"material-icons position-absolute top-50 end-0 translate-middle-y text-success me-3 fs-8"},"check_circle_outline")],-1),y={class:"mb-5 d-flex justify-content-between align-items-center"},M={class:"form-check mt-2"},A=Object(i["i"])("label",{class:"form-check-label fw-light fs-8",for:"agree"}," 我已同意平台服務規範、隱私權政策、讀冊平台會員服務條款 ",-1),C=Object(i["i"])("span",{class:"material-icons fs-6 me-1"}," error ",-1),E=Object(i["i"])("p",{class:"fs-8"},"請閱讀平台服務規範",-1),U=[C,E],P=Object(i["i"])("button",{type:"submit",class:"btn btn-dark rounded-pill text-white w-100 py-13"}," 註冊 ",-1);function B(e,t,s,C,E,B){var J=Object(i["E"])("auth-mail-modal");return Object(i["x"])(),Object(i["h"])(i["a"],null,[Object(i["i"])("div",r,[Object(i["i"])("div",c,[Object(i["i"])("div",a,[o,Object(i["i"])("div",l,[Object(i["i"])("div",n,[Object(i["i"])("div",d,[Object(i["i"])("form",{onSubmit:t[3]||(t[3]=Object(i["O"])((function(){return B.signUp&&B.signUp.apply(B,arguments)}),["prevent"]))},[Object(i["i"])("div",b,[Object(i["i"])("div",u,[p,Object(i["i"])("div",{class:Object(i["r"])(["errorMessenger text-danger d-flex mt-2",{"d-none":!E.isError}])},[f,Object(i["i"])("p",m,Object(i["H"])(E.ErrorText),1)],2)]),Object(i["i"])("div",h,[Object(i["N"])(Object(i["i"])("input",{type:"email",id:"emailAddress",class:"border-0 rounded-pill ps-3 position-relative form-control-lightYellow py-13",placeholder:"thak_tsheh@gmail.com","aria-label":"thak_tsheh@gmail.com","aria-describedby":"button-addon2",required:"",autofocus:"","onUpdate:modelValue":t[0]||(t[0]=function(e){return E.user.Account=e})},null,512),[[i["J"],E.user.Account]])])]),Object(i["i"])("div",j,[O,Object(i["i"])("div",g,[Object(i["N"])(Object(i["i"])("input",{type:"password",class:"form-control-lightYellow border-0 rounded-pill py-13",id:"signUpPassword",placeholder:"","onUpdate:modelValue":t[1]||(t[1]=function(e){return E.user.Password=e})},null,512),[[i["J"],E.user.Password]])]),k]),Object(i["i"])("div",w,[v,x,Object(i["i"])("div",y,[Object(i["i"])("div",M,[Object(i["i"])("input",{type:"checkbox",class:"form-check-input rounded-0 border-0",id:"agree",ref:"checkAgreeBtn",onClick:t[2]||(t[2]=function(){return B.checkAgreeBtn&&B.checkAgreeBtn.apply(B,arguments)})},null,512),A,Object(i["i"])("div",{class:Object(i["r"])(["errorMessenger text-danger d-flex mt-2",{"d-none":E.isCheck}])},U,2)])])]),P],32)])])])])])]),Object(i["l"])(J,{ref:"AuthMailModal"},null,512)],64)}var J={class:"modal fade",id:"exampleModal",tabindex:"-1","aria-labelledby":"exampleModalLabel","aria-hidden":"true",ref:"modal"},T={class:"modal-dialog modal-dialog-centered"},_={class:"modal-content px-8 py-5 rounded-4 position-relative shadow-sm p-3 mb-5 bg-body rounded"},$=Object(i["i"])("p",{class:"fs-4 text-dark mb-4 text-center"},"恭喜您成功註冊會員！",-1),Y=Object(i["i"])("p",{class:"text-dark mb-4"}," 系統已發送驗證信至您的信箱，請至電子信箱驗證信箱以啟用會員資格，方可享有參加活動之權利。 ",-1),H=Object(i["k"])(" 回到首頁 ");function N(e,t,s,r,c,a){var o=Object(i["E"])("router-link");return Object(i["x"])(),Object(i["h"])("div",J,[Object(i["i"])("div",T,[Object(i["i"])("div",_,[Object(i["l"])(o,{to:"/study-circle",type:"button",class:"btn-close position-absolute top-29 end-29",onClick:t[0]||(t[0]=function(e){return a.hideModal()}),"aria-label":"Close"}),$,Y,Object(i["l"])(o,{to:"/study-circle",type:"button",class:"btn btn-dark rounded-pill text-white py-13",onClick:t[1]||(t[1]=function(e){return a.hideModal()})},{default:Object(i["M"])((function(){return[H]})),_:1})])])],512)}var S=s("7c2b"),V=s.n(S),q={data:function(){return{modal:{}}},methods:{showModal:function(){this.modal.show()},hideModal:function(){this.modal.hide()}},mounted:function(){this.modal=new V.a(this.$refs.modal)}},D=s("6b0d"),L=s.n(D);const z=L()(q,[["render",N]]);var F=z,G={data:function(){return{user:{Account:"",Password:""},userTokenData:{JwtToken:""},isError:!1,isCheck:!0,ErrorText:""}},components:{AuthMailModal:F},methods:{signUp:function(){var e=this,t=this.$refs.checkAgreeBtn,s=t.checked;!0===s?(console.log("sign up"),this.$apiHelper.post("api/users/sign-up",this.user).then((function(t){t.data.Status?(console.log(t.data.Message),e.$refs.AuthMailModal.showModal(),e.isError=!1):(console.log(t.data.Message),e.isError=!0,e.ErrorText=t.data.Message)}))):this.isCheck=!1},checkAgreeBtn:function(){!1===this.isCheck&&(this.isCheck=!0)}}};const I=L()(G,[["render",B]]);t["default"]=I}}]);
//# sourceMappingURL=chunk-2d0aba5b.b2089641.js.map