(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2d22c33f"],{f1db:function(t,e,a){"use strict";a.r(e);var s=a("7a23"),o={class:"container"},l={class:"row py-10"},r={class:"col-md-3"},c={class:"px-40 py-5 border border-dark rounded-6"},n={class:"text-center"},i=["src"],b={class:"text-center text-dark mb-1 fw-bold fs-4 lh-36"},d={class:"text-center text-dark mb-3 fw-bold fs-4 lh-36"},u={class:"text-dark fw-light lh-base mb-3"},p=Object(s["i"])("span",{class:"material-icons fs-6 me-2"},"done_outline",-1),j=Object(s["i"])("span",{class:"material-icons fs-6 me-2"},"edit",-1),O=Object(s["k"])(" 編輯個人檔案 "),f=[j,O],h={class:"address py-32 border-dark border-top border-bottom"},g={class:"d-flex justify-content-start align-items-center text-dark m-0"},m=Object(s["i"])("span",{class:"material-icons me-2"},"place",-1),w={class:"aboutMe py-32"},k=Object(s["i"])("h3",{class:"text-dark fs-6 mb-2"},"關於我",-1),v={class:"text-dark mb-32 fw-light"},x=Object(s["i"])("h3",{class:"text-dark fs-6 mb-2"},"我的專長",-1),I={class:"text-dark mb-32 fw-light"},F=Object(s["i"])("h3",{class:"text-dark fs-6 mb-2"},"我的興趣",-1),y={class:"text-dark mb-0 fw-light"},U={class:"py-32 border-dark border-top"},A=Object(s["i"])("h3",{class:"text-dark fs-6 mb-4"},"社群軟體",-1),H={class:"list-unstyled d-flex justify-content-between mb-0"},M={class:"nav-item py-0"},S=["href"],D=Object(s["i"])("i",{class:"fab fa-facebook-square"},null,-1),$=[D],T={class:"nav-item py-0"},J=["href"],C=Object(s["i"])("i",{class:"fab fa-instagram"},null,-1),N=[C],E={class:"nav-item py-0"},L=["href"],P=Object(s["i"])("i",{class:"fab fa-line"},null,-1),_=[P],Y={class:"text-dark fs-7 pt-32 border-top border-dark mb-2 fw-light"},q={class:"text-dark fw-light"},V={class:"col-md-9 px-13"},z=Object(s["k"])("我的活動"),B=Object(s["k"])("讀冊夥伴");function G(t,e,a,j,O,D){var C=Object(s["E"])("component-navbar"),P=Object(s["E"])("router-link"),G=Object(s["E"])("router-view"),K=Object(s["E"])("component-footer");return Object(s["x"])(),Object(s["h"])(s["a"],null,[Object(s["l"])(C),Object(s["i"])("div",o,[Object(s["i"])("div",l,[Object(s["i"])("div",r,[Object(s["i"])("div",c,[Object(s["i"])("div",n,[Object(s["i"])("img",{src:O.profileObj.UserImgUrl,alt:"memberPhoto",class:"rounded-pill memberPhoto-120 mb-4 text-center"},null,8,i),Object(s["i"])("h2",b,Object(s["H"])(O.profileObj.Name),1),Object(s["i"])("p",d,Object(s["H"])(O.profileObj.NickName),1),Object(s["i"])("p",u,Object(s["H"])(O.profileObj.FollowersNumber)+" 追蹤者｜"+Object(s["H"])(O.profileObj.FollowingNumber)+" 關注中 ",1),Object(s["i"])("button",{type:"button",class:Object(s["r"])(["btn btn-dark fw-light rounded-pill d-flex align-items-center text-center mx-auto text-white fs-7 mb-32",{"d-none":O.userAttendObj.Status}]),onClick:e[0]||(e[0]=function(t){return D.changeFollowStatus(O.profileObj.Id)})},[p,Object(s["k"])(" "+Object(s["H"])(O.userAttendObj.Fallowed),1)],2),Object(s["i"])("button",{type:"button",class:Object(s["r"])(["btn btn-dark fw-light rounded-pill d-flex align-items-center text-center mx-auto text-white fs-7 mb-32",{"d-none":!O.userAttendObj.Status}])},f,2)]),Object(s["i"])("div",h,[Object(s["i"])("p",g,[m,Object(s["k"])(" "+Object(s["H"])(O.profileObj.Country)+"．"+Object(s["H"])(O.profileObj.City)+"．"+Object(s["H"])(O.profileObj.Area),1)])]),Object(s["i"])("div",w,[k,Object(s["i"])("p",v,Object(s["H"])(O.profileObj.AboutMe),1),x,Object(s["i"])("p",I,Object(s["H"])(O.profileObj.MySkill),1),F,Object(s["i"])("p",y,Object(s["H"])(O.profileObj.MyInterest),1)]),Object(s["i"])("div",U,[A,Object(s["i"])("ul",H,[Object(s["i"])("li",M,[Object(s["i"])("a",{class:"nav-link text-dark p-0 fs-3",href:O.profileObj.FacebookLink},$,8,S)]),Object(s["i"])("li",T,[Object(s["i"])("a",{class:"nav-link text-dark p-0 fs-3",href:O.profileObj.InstagramLink},N,8,J)]),Object(s["i"])("li",E,[Object(s["i"])("a",{class:"nav-link text-dark p-0 fs-3",href:O.profileObj.LineLink},_,8,L)])])]),Object(s["i"])("p",Y," 於 "+Object(s["H"])(O.profileObj.transCreatDate)+" 加入 ",1),Object(s["i"])("p",q," 瀏覽次數："+Object(s["H"])(O.profileObj.Views),1)])]),Object(s["i"])("div",V,[Object(s["l"])(P,{to:"/profile/my-activity/".concat(this.routeUserId),class:"mb-32 fw-bold fs-4 text-dark me-4",type:"button"},{default:Object(s["M"])((function(){return[z]})),_:1},8,["to"]),Object(s["l"])(P,{to:"/profile/study-partner/".concat(this.routeUserId),class:"mb-32 fw-bold fs-4 text-dark me-4",type:"button"},{default:Object(s["M"])((function(){return[B]})),_:1},8,["to"]),Object(s["l"])(G)])])]),Object(s["l"])(K)],64)}a("99af");var K=a("d592"),Q=a("3364"),R={props:["UserId"],data:function(){return{userAttendObj:{},profileObj:{},routeUserId:"",putFollowStaus:{FollowingUserId:""}}},components:{componentNavbar:K["a"],componentFooter:Q["a"]},watch:{"$route.params.UserId":"changePath"},created:function(){var t=this;console.log(this.$route),this.routeUserId=this.$route.params.UserId,console.log(this.UserId),this.$apiHelper.get("api/users/profile/".concat(this.UserId)).then((function(e){if(e.data.Status){console.log(e.data);var a=e.data.Data,s="".concat("https://thak-tsheh.rocket-coding.com/upload/profile","/").concat(e.data.Data.Image,"?2021");a.UserImgUrl=s,t.transDate(a),t.profileObj=a,console.log(t.profileObj)}})),this.$apiHelper.get("api/users/activity/attend/profile/status/".concat(this.UserId)).then((function(e){var a=e.data.JwtToken;if(t.userAttendObj=e.data,e.data.Status)console.log(e.data.Message),localStorage.setItem("JwtToken",a);else{console.log(e.data.Message);var s=e.data.Message;"非本人資料"===s?(localStorage.setItem("JwtToken",a),!0===e.data.Following?t.userAttendObj.Fallowed="已追蹤":t.userAttendObj.Fallowed="追蹤我"):t.userAttendObj.Fallowed="追蹤我"}console.log(t.userAttendObj)})),this.$apiHelper.put("api/users/profile/views/".concat(this.UserId)).then((function(t){t.data.Status&&console.log(t.data.Message)}))},methods:{changePath:function(){var t=this;console.log(this.$route),this.routeUserId=this.$route.params.UserId,this.$apiHelper.get("api/users/activity/attend/profile/status/".concat(this.routeUserId)).then((function(e){var a=e.data.JwtToken;if(t.userAttendObj=e.data,e.data.Status)console.log(e.data.Message),localStorage.setItem("JwtToken",a);else{console.log(e.data.Message);var s=e.data.Message;"非本人資料"===s?(localStorage.setItem("JwtToken",a),!0===e.data.Following?t.userAttendObj.Fallowed="已追蹤":t.userAttendObj.Fallowed="追蹤我"):t.userAttendObj.Fallowed="追蹤我"}console.log(t.userAttendObj)})),this.$apiHelper.get("api/users/profile/".concat(this.routeUserId)).then((function(e){if(e.data.Status){console.log(e.data);var a=e.data.Data,s="".concat("https://thak-tsheh.rocket-coding.com/upload/profile","/").concat(e.data.Data.Image,"?2021");a.UserImgUrl=s,t.transDate(a),t.profileObj=a,console.log(t.profileObj)}}))},transDate:function(t){var e=t.CreatDate,a=this.splitDate(e);return t.transCreatDate=a.splitFinalDate,t.transCreatTime=a.splitFinalTime,t},splitDate:function(t){var e=new Date(t);e.getFullYear(),e.getMonth(),e.getDate(),e.getHours(),e.getMinutes();var a="".concat(e.getFullYear(),".").concat(e.getMonth()+1,".").concat(e.getDate()),s="".concat(e.getHours(),":").concat((e.getMinutes()<10?"0":"")+e.getMinutes());return{splitFinalDate:a,splitFinalTime:s}},changeFollowStatus:function(t){var e=this;console.log("followerId",t),this.putFollowStaus.FollowingUserId=t,this.$apiHelper.put("api/users/follow/someone",this.putFollowStaus).then((function(t){var a=t.data.JwtToken;t.data.Status?(console.log(t.data.Message),localStorage.setItem("JwtToken",a),"已追蹤"===e.userAttendObj.Fallowed?e.userAttendObj.Fallowed="追蹤我":"追蹤我"===e.userAttendObj.Fallowed&&(e.userAttendObj.Fallowed="已追蹤")):(console.log(t.data.Message),e.$router.push("login")),console.log(e.userAttendObj)}))}}},W=a("6b0d"),X=a.n(W);const Z=X()(R,[["render",G]]);e["default"]=Z}}]);
//# sourceMappingURL=chunk-2d22c33f.b4f893ec.js.map