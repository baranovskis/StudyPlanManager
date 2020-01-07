<template>
    <div class="profile-page">
        <section class="section-profile-cover section-shaped my-0">
            <div class="shape shape-style-1 shape-primary shape-skew alpha-4">
                <span></span>
                <span></span>
                <span></span>
                <span></span>
                <span></span>
                <span></span>
                <span></span>
            </div>
        </section>
        <section class="section section-skew">
          <div class="container">
            <card shadow class="card-profile mt--300 px-4 py-4" no-body>
              <div class="row">
                <div class="col-sm-12">
                  <slim-grid
                    :data="gridData"
                    :editable="true"
                    :autoEdit="true"
                    :grouping="gridGrouping"
                    :column-options="columnOptions"
                    :show-pager="false"
                    :showHeaderRow="false"
                    :forceFitColumns="true"
                    v-on:cell-change="doValidate">
                  </slim-grid>
                </div>
              </div>
            </card>
          </div>
        </section>
    </div>
</template>
<script>
import { Data, Editors } from 'slickgrid-es6';
import SlimGrid from 'vue-slimgrid';
import StudyRepository from '../repositories/StudyRepository';

export default {
  components: { SlimGrid },
  data() {
    return {
      gridData: [],

      gridGrouping: [
        {
          getter: 'studyCourse',
          aggregators: [
            new Data.Aggregators.Sum('class_10'),
            new Data.Aggregators.Sum('class_11'),
            new Data.Aggregators.Sum('class_12'),
          ],
          aggregateCollapsed: false,
          lazyTotalsCalculation: true
        },
        {
          getter: 'studyGroup',
          formatter(g) {
            return 'Macibu joma: ' + g.value + ' <span style="color:green">(' + g.count + ' items)</span>';
          },
          aggregators: [
            new Data.Aggregators.Sum('class_10'),
            new Data.Aggregators.Sum('class_11'),
            new Data.Aggregators.Sum('class_12'),
          ],
          aggregateCollapsed: false,
          lazyTotalsCalculation: true
        }
      ],

      columnOptions: {
        // Set to all columns
        '*': {
          headerFilter: false,
          sortable: false,
        },

        'studyCourse': {
          hidden: true,
        },

        'studyGroup': {
          hidden: true,
        },

        'studyName': {
          name: "Priekšmets",
          minWidth: 150
        },

        'class_0': {
          name: "10. klasse",
          editor: Editors.Text,
          groupTotalsFormatter(totals, columnDef) {
            let val = totals.sum && totals.sum[columnDef.field];
            if (val != null) {
              return 'Kopā: ' + ((Math.round(parseFloat(val)*100)/100));
            }
            return '';
          }
        },

        'class_1': {
          name: "11. klasse",
          editor: Editors.Text,
          groupTotalsFormatter(totals, columnDef) {
            let val = totals.sum && totals.sum[columnDef.field];
            if (val != null) {
              return 'Kopā: ' + ((Math.round(parseFloat(val)*100)/100));
            }
            return '';
          }
        },

        'class_2': {
          name: "12. klasse",
          editor: Editors.Text,
          groupTotalsFormatter(totals, columnDef) {
            let val = totals.sum && totals.sum[columnDef.field];
            if (val != null) {
              return 'Kopā: ' + ((Math.round(parseFloat(val)*100)/100));
            }
            return '';
          }
        },
      },
    };
  },

  mounted () {
    this.doLoad()
  },

  methods: {
    doLoad() {
      StudyRepository
        .get()
        .then(response => {
          this.gridData = [];

          for (let i = 0; i < response.data.length; i++) {
            let data = response.data[i];

            // If groups > 0
            if (data.groups.length > 0) {
              for (let y = 0; y < data.groups.length; y++) {
                let groupData = data.groups[y];

                // If studies > 0
                if (groupData.studies.length > 0) {
                  for (let n = 0; n < groupData.studies.length; n++) {
                    let studyData = groupData.studies[n];

                    let row = {
                      id: studyData.treeId,
                      studyCourse: data.courseName,
                      studyGroup: groupData.groupName,
                      studyName: studyData.studyName,
                    };

                    // Class 10 -> 12
                    for (let c = 0; c < studyData.creditPoints.length; c++) {
                      row["class_" + c] = studyData.creditPoints[c];
                    }

                    this.gridData.push(row);
                  }
                }
                else {
                  this.gridData.push({
                    id: groupData.treeId,
                    studyCourse: data.courseName,
                    studyGroup: groupData.groupName
                  });
                }
              }
            }
            else {
              this.gridData.push({
                id: data.treeId,
                studyCourse: data.courseName
              });
            }
          }
        })
        .catch(error => {
          console.log(error)
          //this.errored = true
        })
        //.finally(() => this.loading = false)
    },

    doValidate(e, args) {
      StudyRepository
        .update({
          column: args.slim.column,
          value: args.slim.value
        }, args.slim.pk)
        .then(response => {
          console.log("Cell changed!");
        })
        .catch(error => {
          console.log(error);
          this.doLoad();
        });
    }
  }
}
</script>
<style src="vue-slimgrid/dist/slimgrid.css"></style>
<style>
  input.editor-text {
    position: absolute;
    width: 100%;
    height: 100%;
    border: 0;
    margin: 0;
    background: transparent;
    padding: 2px 3px 2px 3px;
    transform: translate(-3px, -2px);
  }
</style>
