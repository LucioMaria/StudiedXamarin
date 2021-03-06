﻿using System;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using FirebaseProject.Models;
using System.Collections.Generic;
using Firebase.Firestore;
using Android.Content;
using Firebase;
using Java.Text;
using Java.Util;

namespace FirebaseProject.Adapter
{
    class ExamAdapter : RecyclerView.Adapter 
    {
        public Context context;
        public Android.Support.V7.Widget.RecyclerView recyclerView;
        public event EventHandler<ExamAdapterClickEventArgs> ItemClick;
        public event EventHandler<ExamAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<ExamAdapterClickEventArgs> Exam_NameClick;
        List<ExamModel> Items;
        public Button cfuButton;
        public ImageButton selectButton;
        public TextView examNameTV, examDateTV;

        /* public FirebaseFirestore GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(this.context);
            FirebaseFirestore database;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                .SetProjectId("fir-project-16446")
                .SetApplicationId("fir-project-16446")
                .SetApiKey("AIzaSyB4tFXBV6P6AiHCZmsjNNEWlF_9eXncoQg")
                .SetDatabaseUrl("https://fir-project-16446.firebaseio.com")
                .SetStorageBucket("fir-project-16446.appspot.com")
                .Build();

                app = FirebaseApp.InitializeApp(this.context, options);
                database = FirebaseFirestore.GetInstance(app);
            }
            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }
            return database;
        } */



        public ExamAdapter(Context context, Android.Support.V7.Widget.RecyclerView recyclerView, List<ExamModel>Exams)
        {
            this.context = context;
            this.recyclerView = recyclerView;
            Items = Exams;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemview = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.examrow, parent, false);
            var vh = new ExamAdapterViewHolder(itemview, OnClick, OnLongClick, OnExam_NameClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var exam = Items[position];
            var holder = viewHolder as ExamAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.examNameTV.Text = exam.examName;
            DateFormat df = new SimpleDateFormat("dd/MM/yy", Locale.Italy);
            // string fdate = df.Format(Items[position].get_exam_date().ToDate());
            // holder.examDateTV.Text = fdate;                
            // holder.examNameTV.Click += ItemView_Click;
            
        }

        /* private void ItemView_Click(object sender, EventArgs e)
         {

            int position = this.recyclerView.GetChildAdapterPosition((View)sender);
            ExamModel examname_clicked = this.Items[position];
            string examname = examname_clicked.get_exam_name();
            DocumentReference docRef = GetDatabase().Collection("exams").Document(examname);
            docRef.Update("examname", "Update");
        } */

        public override int ItemCount => Items.Count;

        void OnClick(ExamAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ExamAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnExam_NameClick(ExamAdapterClickEventArgs args) => Exam_NameClick?.Invoke(this, args);
    }

    public class ExamAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView examNameTV { get; set; }
        public TextView examDateTV { get; set; }      
        public Button cfuButton { get; set; }
        public ImageButton selectButton { get; set; }
     
     

        public ExamAdapterViewHolder(View itemView, Action<ExamAdapterClickEventArgs> clickListener,
                            Action<ExamAdapterClickEventArgs> longClickListener, Action<ExamAdapterClickEventArgs> nameClickListener) : base(itemView)
        {
            //TextView = v;
            int position = AdapterPosition;
            examNameTV = (TextView)itemView.FindViewById(Resource.Id.exam_name_tv);
            examDateTV = (TextView)itemView.FindViewById(Resource.Id.exam_date_tv);
            cfuButton = (Button)itemView.FindViewById(Resource.Id.exam_cfu_button);
            selectButton = (ImageButton)itemView.FindViewById(Resource.Id.select_arrow);
            examNameTV.Click += (sender, e) => nameClickListener(new ExamAdapterClickEventArgs { View = itemView, Position = AdapterPosition });


            itemView.Click += (sender, e) => clickListener(new ExamAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new ExamAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            
            
        }

      /*  private void ExamNameTV_Click(object sender, ExamAdapterClickEventArgs e)
        {
            
            ExamModel examname_clicked = this.Items[e.Position];
            string examname = examname_clicked.get_exam_name();
            DocumentReference docRef = GetDatabase().Collection("exams").Document(examname);
            docRef.Update("examname", "Update");
        } */
    }

    public class ExamAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}